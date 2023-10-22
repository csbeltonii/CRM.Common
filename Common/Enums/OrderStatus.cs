using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "Created")]
    Created,

    [EnumMember(Value = "Invoiced")]
    Invoiced,

    [EnumMember(Value = "Processing")]
    Processing,

    [EnumMember(Value = "Processed")]
    Processed,

    [EnumMember(Value = "Shipped")]
    Shipped,

    [EnumMember(Value = "Fulfilled")]
    Fulfilled
}