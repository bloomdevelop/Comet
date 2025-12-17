using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace StoatApplication.Core.Api.Models;

public sealed record Root(
    [property: JsonPropertyName(("revolt"))]
    string Version,
    [property: JsonPropertyName(("features"))]
    Features Features,
    [property: JsonPropertyName("ws")] string WebSocketUrl,
    [property: JsonPropertyName("app")] string AppUrl,
    [property: JsonPropertyName("vapid")] string VapidPublicKey,
    [property: JsonPropertyName("build")] Build Build
);

public sealed record Features(
    [property: JsonPropertyName("captcha")]
    CaptchaFeature Captcha,
    [property: JsonPropertyName("email")] bool IsEmailEnabled,
    [property: JsonPropertyName("invite_only")]
    bool IsInviteOnly,
    [property: JsonPropertyName("autumn")] AutumnFeature Autumn,
    [property: JsonPropertyName("january")]
    JanuaryFeature January,
    [property: JsonPropertyName("livekit")]
    LiveKitFeature LiveKit
);

public sealed record CaptchaFeature(
    [property: JsonPropertyName("enabled")]
    bool IsEnabled,
    [property: JsonPropertyName("key")] string Key
);

public sealed record AutumnFeature(
    [property: JsonPropertyName("enabled")]
    bool IsEnabled,
    [property: JsonPropertyName("url")] string Url
);

public sealed record JanuaryFeature(
    [property: JsonPropertyName("enabled")]
    bool IsEnabled,
    [property: JsonPropertyName("url")] string Url
);

public sealed record LiveKitFeature(
    [property: JsonPropertyName("enabled")]
    bool IsEnabled,
    [property: JsonPropertyName("nodes")] List<LiveKitNode> Nodes
);

public sealed record LiveKitNode(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("lat")] double Latitude,
    [property: JsonPropertyName("lon")] double Longitude,
    [property: JsonPropertyName("public_url")]
    string Url
);

public sealed record Build(
    [property: JsonPropertyName("commit_sha")]
    string CommitSha,
    [property: JsonPropertyName("commit_timestamp")]
    string CommitTimestamp,
    [property: JsonPropertyName("semver")] string SemVer,
    [property: JsonPropertyName("origin_url")]
    string OriginUrl,
    [property: JsonPropertyName("timestamp")]
    string Timestamp
);