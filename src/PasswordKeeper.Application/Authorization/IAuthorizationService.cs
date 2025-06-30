namespace PasswordKeeper.Application.Authorization;

public interface IAuthorizationService
{
    public Task<string> Login(string username, string password, CancellationToken cancellationToken = default);

    public Task Logout(CancellationToken cancellationToken = default);
}