using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ClientToServer;

public sealed record Ping(
    [property: JsonPropertyName("data")] long Data
) : IEvent
{
    public string Type => "Ping";
}