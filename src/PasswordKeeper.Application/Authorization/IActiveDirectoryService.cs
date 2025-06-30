namespace PasswordKeeper.Application.Authorization;

public interface IActiveDirectoryService
{
    public Task Login(string username, string password, CancellationToken cancellationToken = default);
    public Task Logout();
}