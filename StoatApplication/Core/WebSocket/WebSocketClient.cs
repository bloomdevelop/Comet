using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using StoatApplication.Core.Api.Endpoints;
using StoatApplication.Core.Logging;
using Websocket.Client;

namespace StoatApplication.Core.WebSocket;

public sealed class WebSocketClient(Uri serverUrl) : IAsyncDisposable
{
    private readonly WebsocketClient _client = new(serverUrl)
    {
        ReconnectTimeout = TimeSpan.FromSeconds(10)
    };

    private readonly ILogger<WebSocketClient> _logger = Logger.Create<WebSocketClient>();

    public async ValueTask DisposeAsync()
    {
        if (_client.IsStarted)
            await DisconnectAsync().ConfigureAwait(false);

        _client.Dispose();
    }

    public static async Task<WebSocketClient> CreateFromConfigAsync(CancellationToken ct = default)
    {
        var log = Logger.Create<WebSocketClient>();
        log.LogInformation("Creating WebSocketClient from server configuration");
        var config = await Root.GetServerConfiguration();
        ct.ThrowIfCancellationRequested();

        return new WebSocketClient(new Uri(config.WebSocketUrl));
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