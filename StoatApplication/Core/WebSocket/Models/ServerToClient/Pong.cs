using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record Pong(
    [property: JsonPropertyName("data")] int Data
) : IEvent
{
    public string Type => "Pong";
}