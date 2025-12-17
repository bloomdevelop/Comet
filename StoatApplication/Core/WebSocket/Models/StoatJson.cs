using System.Text.Json;
using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models;

public static class StoatJson
{
    public static readonly JsonSerializerOptions WebSocketOptions = new(JsonSerializerDefaults.Web)
    {
        Converters =
        {
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
            new StoatEventJsonConverter()
        }
    };
}