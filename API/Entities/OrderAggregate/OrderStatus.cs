using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Entities.OrderAggregate;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    [EnumMember(Value = "OrderReceived")]
    OrderReceived,
    [EnumMember(Value = "OrderConfirmed")]
    OrderConfirmed,
    [EnumMember(Value = "OrderShipped")]
    OrderShipped,
    [EnumMember(Value = "OrderDelivered")]
    OrderDelivered,
}