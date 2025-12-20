using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StoatApplication.Core.Api.Models;

[JsonConverter(typeof(StringEnumConverter))]
public enum MessageEmbedType
{
    [EnumMember(Value = "Website")] Website,
    [EnumMember(Value = "Image")] Image,
    [EnumMember(Value = "Video")] Video,
    [EnumMember(Value = "Text")] Text,
    [EnumMember(Value = "None")] None
}