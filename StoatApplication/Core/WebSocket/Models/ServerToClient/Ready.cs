using System.Collections.Generic;
using System.Text.Json.Serialization;
using StoatApplication.Core.Api.Models;

namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public record Ready(
    [property: JsonPropertyName("users")] List<User>? Users,
    [property: JsonPropertyName("servers")]
    List<object>? Servers,
    [property: JsonPropertyName("channels")]
    List<object>? Channels,
    [property: JsonPropertyName("members")]
    List<object>? Members,
    [property: JsonPropertyName("emojis")] List<object>? Emojis,
    [property: JsonPropertyName("user_settings")]
    object? UserSettings,
    [property: JsonPropertyName("channel_unreads")]
    List<object>? ChannelUnreads,
    [property: JsonPropertyName("policy_changes")]
    List<object>? PolicyChanges
) : IEvent
{
    public string Type => "Ready";
}