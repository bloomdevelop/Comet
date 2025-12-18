using System.Text.Json;

namespace StoatApplication.Core.WebSocket.Models;

/// <summary>
///     Base interface for any websocket event (client->server or server->client).
/// </summary>
public interface IEvent
{
    string Type { get; }
}

/// <summary>
///     A generic fallback to avoid crashing the client
/// </summary>
/// <param name="Type">Event Type</param>
/// <param name="Raw">Raw Json Data </param>
public sealed record UnknownEvent(string Type, JsonElement Raw) : IEvent;