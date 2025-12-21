using System;
using System.Net.WebSockets;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Api;
using StoatApplication.Core.Api.Endpoints;
using StoatApplication.Core.Logging;
using StoatApplication.Core.WebSocket.Models;
using StoatApplication.Core.WebSocket.Models.ClientToServer;
using StoatApplication.Core.WebSocket.Models.ServerToClient;
using Websocket.Client;

namespace StoatApplication.Core.WebSocket;

public sealed class WebSocketClient : IAsyncDisposable
{
    private readonly WebsocketClient _client;
    private readonly ILogger<WebSocketClient> _logger = Logger.Create<WebSocketClient>();
    private IDisposable? _messageSubscription;
    private IDisposable? _pingSubscription;

    static WebSocketClient()
    {
        // Register Server to Client events
        EventTypeRegistry.Register<Authenticated>("Authenticated");
        EventTypeRegistry.Register<Error>("Error");
        EventTypeRegistry.Register<LoggedOut>("LoggedOut");
        EventTypeRegistry.Register<Message>("Message");
        EventTypeRegistry.Register<Pong>("Pong");
        EventTypeRegistry.Register<Ready>("Ready");

        // Register Client to Server events (for serialization/deserialization)
        EventTypeRegistry.Register<Authenticate>("Authenticate");
        EventTypeRegistry.Register<BeginTyping>("BeginTyping");
        EventTypeRegistry.Register<EndTyping>("EndTyping");
        EventTypeRegistry.Register<Ping>("Ping");
    }

    private WebSocketClient(Uri serverUrl)
    {
        _client = new WebsocketClient(serverUrl)
        {
            ReconnectTimeout = TimeSpan.FromSeconds(10)
        };

        _messageSubscription = _client.MessageReceived
            .Where(msg => msg.MessageType == WebSocketMessageType.Text)
            .Select(msg =>
            {
                _logger.LogDebug("Received raw message: {Message}", msg.Text);
                try
                {
                    return JsonSerializer.Deserialize<IEvent>(msg.Text!, EventJson.WebSocketOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to deserialize message: {Message}", msg.Text);
                    return null;
                }
            })
            .Where(evt => evt != null)
            .Subscribe(evt =>
            {
                _logger.LogInformation("Received event: {Type}", evt!.Type);
                OnEventReceived?.Invoke(evt);
            });
        _pingSubscription = Observable.Interval(TimeSpan.FromSeconds(10))
            .Subscribe(_ => SendPing());
    }

    public async ValueTask DisposeAsync()
    {
        _messageSubscription?.Dispose();
        _messageSubscription = null;
        _pingSubscription?.Dispose();
        _pingSubscription = null;

        if (_client.IsStarted)
            await DisconnectAsync().ConfigureAwait(false);

        _client.Dispose();
    }

    private void SendPing()
    {
        if (!_client.IsStarted) return;

        var pingEvent = new Ping(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds());
        var json = JsonSerializer.Serialize(pingEvent, EventJson.WebSocketOptions);

        _logger.LogDebug("Sending ping: {Json}", json);
        _client.Send(json);
    }

    public event Action<IEvent>? OnEventReceived;

    public static async Task<WebSocketClient> CreateFromConfigAsync(CancellationToken ct = default)
    {
        var log = Logger.Create<WebSocketClient>();
        log.LogInformation("Creating WebSocketClient from server configuration");
        var config = await Root.GetServerConfiguration();
        ct.ThrowIfCancellationRequested();
        var token = SessionManager.CurrentSession?.Token;

        return new WebSocketClient(new Uri($"{config.WebSocketUrl}?version=1&format=json&token={token}"));
    }

    public async Task ConnectAsync(CancellationToken ct = default)
    {
        if (_client.IsStarted)
            return;
        _logger.LogInformation("Starting WebSocket connection to {Url}", _client.Url);
        await _client.Start().ConfigureAwait(false);
        _logger.LogInformation("WebSocket connected");
        ct.ThrowIfCancellationRequested();
    }

    private async Task DisconnectAsync()
    {
        if (!_client.IsStarted)
            return;
        _logger.LogInformation("Stopping WebSocket connection");
        await _client.Stop(WebSocketCloseStatus.NormalClosure, "Disconnected Manually").ConfigureAwait(false);
        _logger.LogInformation("WebSocket disconnected");
    }
}