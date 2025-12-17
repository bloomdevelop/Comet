using System.Text.Json.Serialization;

namespace StoatApplication.Core.Api.Models;

public record Auth
{
    public sealed record LoginResponse(
        [property: JsonPropertyName("result")] string Result,
        [property: JsonPropertyName("_id")] string Id,
        [property: JsonPropertyName("user_id")]
        string UserId,
        [property: JsonPropertyName("token")] string Token,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("last_seen")]
        string LastSeen,
        [property: JsonPropertyName("origin")] string? Origin,
        [property: JsonPropertyName("subscriptions")]
        Subcriptions Subcriptions
    );

    public sealed record Subcriptions(
        [property: JsonPropertyName("endpoint")]
        string Endpoint,
        [property: JsonPropertyName("p256dh")] string P256dh,
        [property: JsonPropertyName("auth")] string Auth
    );

    public sealed record Session(
        [property: JsonPropertyName("_id")] string Id,
        [property: JsonPropertyName("name")] string Name
    );
}