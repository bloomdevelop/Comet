using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace StoatApplication.Core.Api.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserPresence
{
    [EnumMember(Value = "Online")] Online,
    [EnumMember(Value = "Idle")] Idle,
    [EnumMember(Value = "Focus")] Focus,
    [EnumMember(Value = "Busy")] Busy,
    [EnumMember(Value = "Invisible")] Invisible
}