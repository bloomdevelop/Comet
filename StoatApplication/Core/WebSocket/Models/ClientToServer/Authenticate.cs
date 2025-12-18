using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ClientToServer;

/// <summary>
///     Represents an authentication event sent from the client to the server.
/// </summary>
/// <remarks>
///     This class encapsulates the authentication token provided by the client
///     and identifies the event type as "Authenticate". The token is used on the
///     server side to verify the client's identity and proceed with the
///     authentication process.
/// </remarks>
public sealed record Authenticate(
    [property: JsonPropertyName("token")] string Token
) : IStoatEvent
{
    /// <summary>
    ///     Gets the type of the event associated with the object.
    ///     The value is a string identifier that represents the nature or purpose of the event,
    ///     typically used to determine how the event should be processed or handled.
    ///     For the Authenticate object, this property returns "Authenticate".
    /// </summary>
    public string Type => "Authenticate";
}