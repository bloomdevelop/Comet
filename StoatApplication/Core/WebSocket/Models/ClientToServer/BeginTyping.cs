using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ClientToServer;

public sealed record BeginTyping(
    [property: JsonPropertyName("channel")]
    string ChannelId
) : IStoatEvent
{
    public string Type => "BeginTyping";
}