using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoatApplication.Core.Api.Models;

public sealed record Message(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("nonce")] string? Nonce,
    [property: JsonPropertyName("channel")]
    string ChannelId,
    [property: JsonPropertyName("author")] string AuthorId,
    [property: JsonPropertyName("user")] User? User,
    [property: JsonPropertyName("webhook")]
    MessageWebhook? Webhook,
    [property: JsonPropertyName("content")]
    string Content,
    [property: JsonPropertyName("system")] SystemMessage? SystemMessage,
    [property: JsonPropertyName("attachments")]
    List<MessageAttachment>? Attachments,
    [property: JsonPropertyName("edited")] DateTime Edited,
    [property: JsonPropertyName("embeds")] List<MessageEmbed>? Embeds,
    [property: JsonPropertyName("mentions")]
    List<string>? Mentions,
    [property: JsonPropertyName("role_mentions")]
    List<string>? RoleMentions,
    [property: JsonPropertyName("replies")]
    List<string>? Replies,
    [property: JsonPropertyName("reactions")]
    Dictionary<string, List<string>>? Reactions
);

public abstract record MessageWebhook(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("avatar")] string AvatarUrl
);

public abstract record SystemMessage(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("content")]
    string Content
);

public abstract record MessageAttachment(
    [property: JsonPropertyName("_id")] string Id,
    [property: JsonPropertyName("tag")] string Tag,
    [property: JsonPropertyName("filename")]
    string FileName,
    [property: JsonPropertyName("content_type")]
    string? ContentType,
    [property: JsonPropertyName("size")] int Size,
    [property: JsonPropertyName("deleted")]
    bool? IsDeleted,
    [property: JsonPropertyName("reported")]
    bool? IsReported,
    [property: JsonPropertyName("message_id")]
    string? MessageId,
    [property: JsonPropertyName("user_id")]
    string? UserId,
    [property: JsonPropertyName("server_id")]
    string? ServerId,
    [property: JsonPropertyName("object_id")]
    string? ObjectId
);

public abstract record MessageAttachmentMetadata(
    [property: JsonPropertyName("type")] string Type
);

[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
[JsonDerivedType(typeof(WebsiteEmbed), "Website")]
[JsonDerivedType(typeof(ImageEmbed), "Image")]
[JsonDerivedType(typeof(VideoEmbed), "Video")]
[JsonDerivedType(typeof(TextEmbed), "Text")]
[JsonDerivedType(typeof(NoneEmbed), "None")]
public abstract record MessageEmbed;

public sealed record WebsiteEmbed(
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("original_url")]
    string? OriginalUrl,
    [property: JsonPropertyName("title")] string? Title,
    [property: JsonPropertyName("description")]
    string? Description,
    [property: JsonPropertyName("site_name")]
    string? SiteName,
    [property: JsonPropertyName("icon_url")]
    string? IconUrl,
    [property: JsonPropertyName("colour")] string? Color
) : MessageEmbed;

public sealed record ImageEmbed(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int Width,
    [property: JsonPropertyName("height")] int Height,
    [property: JsonPropertyName("size")] string Size
) : MessageEmbed;

public sealed record VideoEmbed(
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("width")] int Width,
    [property: JsonPropertyName("height")] int Height
) : MessageEmbed;

public sealed record TextEmbed(
    [property: JsonPropertyName("title")] string? Title,
    [property: JsonPropertyName("description")]
    string? Description,
    [property: JsonPropertyName("icon_url")]
    string? IconUrl,
    [property: JsonPropertyName("url")] string? Url,
    [property: JsonPropertyName("colour")] string? Color
) : MessageEmbed;

public sealed record NoneEmbed : MessageEmbed;