using API.Entities.Identity;

namespace API.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}