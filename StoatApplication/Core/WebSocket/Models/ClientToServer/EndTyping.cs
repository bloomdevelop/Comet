using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ClientToServer;

public record EndTyping(
    [property: JsonPropertyName("channel")]
    string ChannelId
) : IStoatEvent
{
    public string Type => "EndTyping";
}