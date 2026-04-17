using EMS.API.Models;

namespace EMS.API.Jwt
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
