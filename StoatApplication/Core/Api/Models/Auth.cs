using System.Text.Json.Serialization;

namespace StoatApplication.Core.Api.Models;

public abstract record Auth
{
    /// <summary>
    ///     Response from <c>/auth/session/login</c>
    /// </summary>
    /// <param name="Result">Not Documented</param>
    /// <param name="Id">Unique Id</param>
    /// <param name="UserId">User Id</param>
    /// <param name="Token">Session token</param>
    /// <param name="Name">Display Name</param>
    /// <param name="LastSeen">When the session was last logged in (iso8601 timestamp)</param>
    /// <param name="Origin">
    ///     What is the session origin? This could be used to differentiate sessions that come from
    ///     staging/test vs. prod, etc. Authifier will set this to None by default. The application must fill it in.
    /// </param>
    /// <param name="Subscription">Web Push Subscription</param>
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
        Subscription Subscription
    );

    /// <summary>
    ///     Web Push Subscription
    /// </summary>
    /// <param name="Endpoint">Not Documented</param>
    /// <param name="P256dh">Not Documented</param>
    /// <param name="Auth">Not Documented</param>
    public abstract record Subscription(
        [property: JsonPropertyName("endpoint")]
        string Endpoint,
        [property: JsonPropertyName("p256dh")] string P256dh,
        [property: JsonPropertyName("auth")] string Auth
    );

    /// <summary>
    ///     Information about a specific session
    /// </summary>
    /// <param name="Id">Session ID</param>
    /// <param name="Name">Session Name</param>
    public sealed record Session(
        [property: JsonPropertyName("_id")] string Id,
        [property: JsonPropertyName("name")] string Name
    );

    /// <summary>
    ///     Represents a response indicating whether onboarding is required for a user.
    /// </summary>
    /// <param name="NeedOnboarding">Determines if the user requires onboarding.</param>
    public sealed record OnboardingResponse(
        [property: JsonPropertyName("onboarding")]
        bool NeedOnboarding
    );
}