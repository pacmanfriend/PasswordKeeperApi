using System.Security.Claims;

namespace PasswordKeeper.Application.Authorization;

public interface IJwtService
{
    string GenerateToken(IEnumerable<Claim> claims);
}
