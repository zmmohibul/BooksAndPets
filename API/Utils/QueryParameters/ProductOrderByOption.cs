using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Utils.QueryParameters;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProductOrderByOption
{
    [EnumMember(Value = "PriceAsc")]
    PriceAsc,
    [EnumMember(Value = "PriceDesc")]
    PriceDesc,
    [EnumMember(Value = "NameAsc")]
    NameAsc,
    [EnumMember(Value = "NameDesc")]
    NameDesc
}