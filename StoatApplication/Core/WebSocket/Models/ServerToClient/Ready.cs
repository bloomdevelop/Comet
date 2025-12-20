using System.Collections.Generic;
using Newtonsoft.Json;
using StoatApplication.Core.Api.Models;

namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public record Ready(
    [property: JsonProperty("users")] List<User>? Users,
    [property: JsonProperty("servers")] List<object>? Servers,
    [property: JsonProperty("channels")] List<object>? Channels,
    [property: JsonProperty("members")] List<object>? Members,
    [property: JsonProperty("emojis")] List<object>? Emojis,
    [property: JsonProperty("user_settings")]
    object? UserSettings,
    [property: JsonProperty("channel_unreads")]
    List<object>? ChannelUnreads,
    [property: JsonProperty("policy_changes")]
    List<object>? PolicyChanges
) : IEvent
{
    public string Type => "Ready";
}