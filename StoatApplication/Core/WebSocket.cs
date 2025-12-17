using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using StoatApplication.Core.Api.Endpoints;
using Websocket.Client;

namespace StoatApplication.Core;

public sealed class WebSocket(Uri serverUrl) : IAsyncDisposable
{
    private readonly WebsocketClient _client = new(serverUrl)
    {
        ReconnectTimeout = TimeSpan.FromSeconds(10)
    };

    public async ValueTask DisposeAsync()
    {
        if (_client.IsStarted)
            await DisconnectAsync().ConfigureAwait(false);

        _client.Dispose();
    }

    public static async Task<WebSocket> CreateFromConfigAsync(CancellationToken ct = default)
    {
        var config = await Root.GetServerConfiguration();
        ct.ThrowIfCancellationRequested();

        return new WebSocket(new Uri(config.WebSocketUrl));
    }

    public async Task ConnectAsync(CancellationToken ct = default)
    {
        if (_client.IsStarted)
            return;

        await _client.Start().ConfigureAwait(false);
        ct.ThrowIfCancellationRequested();
    }

    private async Task DisconnectAsync()
    {
        if (!_client.IsStarted)
            return;

        await _client.Stop(WebSocketCloseStatus.NormalClosure, "Disconnected Manually").ConfigureAwait(false);
    }
}