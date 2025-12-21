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
public sealed class EventJsonConverter : JsonConverter<IEvent>
{
    public override IEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var type = TryGetString(root, "type")
                   ?? TryGetString(root, "t")
                   ?? throw new JsonException("Stoat event missing 'type'/'t'.");

        var data = TryGetElement(root, "data") ?? TryGetElement(root, "d");

        if (!EventTypeRegistry.TryResolve(type, out var clrType) || clrType is null)
            return new UnknownEvent(type, root);

        if (data is null || data.Value.ValueKind == JsonValueKind.Null)
        {
            // If there is no data, try to create an instance of the event type.
            // This handles events like "Pong" which might be just { "type": "pong" }
            try
            {
                return (IEvent)Activator.CreateInstance(clrType)!;
            }
            catch
            {
                return new UnknownEvent(type, root);
            }
        }

        // Most protocols put the payload under data/d; deserialize that into the concrete event type.
        try
        {
            var evt = (IEvent?)JsonSerializer.Deserialize(data.Value.GetRawText(), clrType, options);
            return evt ?? new UnknownEvent(type, root);
        }
        catch (JsonException)
        {
            // If deserializing the 'data' fragment fails (e.g., data is a primitive but clrType is an object),
            // fallback to an UnknownEvent or try to instantiate the type directly if it's a simple signal.
            return new UnknownEvent(type, root);
        }
    }

    public override void Write(Utf8JsonWriter writer, IEvent value, JsonSerializerOptions options)
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