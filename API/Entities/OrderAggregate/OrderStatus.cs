using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Entities.OrderAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "Received")]
    Received,
    [EnumMember(Value = "Confirmed")]
    Confirmed,
    [EnumMember(Value = "Shipped")]
    Shipped,
    [EnumMember(Value = "Delivered")]
    Delivered,
    [EnumMember(Value = "Cancelled")]
    Cancelled,
}