using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace API.Entities.Identity;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserRole
{
    [EnumMember(Value = "User")]
    User,
    [EnumMember(Value = "Admin")]
    Admin,
}