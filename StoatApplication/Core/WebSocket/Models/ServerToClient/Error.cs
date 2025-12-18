using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models.ServerToClient;

public sealed record Error(
    [property: JsonPropertyName("error")] string ErrorId
) : IStoatEvent
{
    public string Type => "Error";
}