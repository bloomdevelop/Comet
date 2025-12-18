using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record Error(
    [property: JsonPropertyName("error")] string ErrorId
) : IEvent
{
    public string Type => "Error";
}