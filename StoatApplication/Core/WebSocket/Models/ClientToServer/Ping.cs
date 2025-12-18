using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ClientToServer;

public sealed record Ping(
    [property: JsonPropertyName("data")] int Data
) : IStoatEvent
{
    public string Type => "Ping";
}