using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoatApplication.Core.Api.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum UserPresence
{
    [EnumMember(Value = "Online")] Online,
    [EnumMember(Value = "Idle")] Idle,
    [EnumMember(Value = "Focus")] Focus,
    [EnumMember(Value = "Busy")] Busy,
    [EnumMember(Value = "Invisible")] Invisible
}