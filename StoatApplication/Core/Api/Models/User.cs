using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoatApplication.Core.Api.Models;

public sealed record User(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("username")]
    string Username,
    [property: JsonPropertyName("display_name")]
    string DisplayName,
    [property: JsonPropertyName("discriminator")]
    string Discriminator,
    [property: JsonPropertyName("avatar")] UserAvatar Avatar,
    [property: JsonPropertyName("badges")] int Badges,
    [property: JsonPropertyName("flags")] int Flags,
    [property: JsonPropertyName("privileged")]
    bool IsPrivileged,
    [property: JsonPropertyName("bot")] UserBot Bot,
    [property: JsonPropertyName("relationship")]
    string Relationship,
    [property: JsonPropertyName("online")] bool IsOnline
);

public sealed record UserAvatar(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("tag")] string Tag,
    [property: JsonPropertyName("filename")]
    string FileName,
    [property: JsonPropertyName("metadata")]
    UserAvatarMetadata Metadata,
    [property: JsonPropertyName("content_type")]
    string ContentType,
    [property: JsonPropertyName("size")] int Size,
    [property: JsonPropertyName("deleted")]
    bool? IsDeleted,
    [property: JsonPropertyName("reported")]
    bool? IsReported,
    [property: JsonPropertyName("message_id")]
    string? MessageId,
    [property: JsonPropertyName("server_id")]
    string? ServerId,
    [property: JsonPropertyName("object_id")]
    string? ObjectId,
    [property: JsonPropertyName("relations")]
    List<UserRelations> Relations
);

public sealed record UserAvatarMetadata(
    [property: JsonPropertyName("type")] string Type
);

public sealed record UserRelations(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("status")] string Status
);

public sealed record UserStatus(
    [property: JsonPropertyName("text")] string? Text,
    [property: JsonPropertyName("presence")]
    UserPresence Presence
);

public sealed record UserBot(
    [property: JsonPropertyName("owner")] string Owner
);