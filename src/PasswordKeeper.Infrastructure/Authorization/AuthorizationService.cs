using System.Security.Claims;
using PasswordKeeper.Application.Authorization;

namespace PasswordKeeper.Infrastructure.Authorization;

public class AuthorizationService : IAuthorizationService
{
    private readonly IActiveDirectoryService _activeDirectoryService;
    private readonly IJwtService _jwtService;

    public AuthorizationService(IActiveDirectoryService activeDirectoryService, IJwtService jwtService)
    {
        _activeDirectoryService = activeDirectoryService;
        _jwtService = jwtService;
    }

    public async Task<string> Login(string username, string password, CancellationToken cancellationToken = default)
    {
        await _activeDirectoryService.Login(username, password, cancellationToken);

        var claims = new List<Claim>
        {
            new("id", username)
        };

        return _jwtService.GenerateToken(claims);
    }

    public Task Logout(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}