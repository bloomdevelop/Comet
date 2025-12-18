using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ClientToServer;

public record EndTyping(
    [property: JsonPropertyName("channel")]
    string ChannelId
) : IEvent
{
    public string Type => "EndTyping";
}