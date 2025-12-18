using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoatApplication.Core.Api.Models.Autumn;

[JsonConverter(typeof(StringEnumConverter))]
public enum Tag
{
    [EnumMember(Value = "attachments")] Attachments,
    [EnumMember(Value = "avatars")] Avatars,
    [EnumMember(Value = "backgrounds")] Background,
    [EnumMember(Value = "icons")] Icons,
    [EnumMember(Value = "banners")] Banners,
    [EnumMember(Value = "emojis")] Emojis
}