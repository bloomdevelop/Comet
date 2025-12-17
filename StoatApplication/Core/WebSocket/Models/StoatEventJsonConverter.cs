using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StoatApplication.Core.WebSocket.Models;

/// <summary>
///     Parses Stoat websocket events that look like:
///     { "type": "...", "data": { ... } }
///     OR (common in gateways) { "t": "...", "d": { ... } }
///     Falls back to UnknownStoatEvent if the type isn't registered.
/// </summary>
public sealed class StoatEventJsonConverter : JsonConverter<IStoatEvent>
{
    public override IStoatEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = TryGetString(root, "type")
                   ?? TryGetString(root, "t")
                   ?? throw new JsonException("Stoat event missing 'type'/'t'.");

        var data = TryGetElement(root, "data") ?? TryGetElement(root, "d");

        if (StoatEventTypeRegistry.TryResolve(type, out var clrType) && clrType is not null)
        {
            if (data is null)
            {
                // Some events may not carry a payload.
                // Try to deserialize from the whole root if the event model expects it.
                var deserialized = (IStoatEvent?)JsonSerializer.Deserialize(root.GetRawText(), clrType, options);
                return deserialized ?? new UnknownStoatEvent(type, root);
            }

            // Most protocols put the payload under data/d; deserialize that into the concrete event type.
            var evt = (IStoatEvent?)JsonSerializer.Deserialize(data.Value.GetRawText(), clrType, options);
            return evt ?? new UnknownStoatEvent(type, root);
        }

        return new UnknownStoatEvent(type, root);
    }

    public override void Write(Utf8JsonWriter writer, IStoatEvent value, JsonSerializerOptions options)
    {
        // Serialize as { "type": "...", "data": { ...event... } } by default.
        writer.WriteStartObject();
        writer.WriteString("type", value.Type);

        writer.WritePropertyName("data");
        JsonSerializer.Serialize(writer, value, value.GetType(), options);

        // Writing is protocol-sensitive (type/data vs. t/d, and whether Type is inside the payload).
        // Implement this once client->server events are defined precisely.
        throw new NotSupportedException(
            "Serializing IStoatEvent is not supported yet. Serialize a concrete envelope instead.");
    }

    private static string? TryGetString(JsonElement obj, string name)
    {
        return obj.ValueKind == JsonValueKind.Object && obj.TryGetProperty(name, out var p) &&
               p.ValueKind == JsonValueKind.String
            ? p.GetString()
            : null;
    }

    private static JsonElement? TryGetElement(JsonElement obj, string name)
    {
        return obj.ValueKind == JsonValueKind.Object && obj.TryGetProperty(name, out var p)
            ? p
            : null;
    }
}