using Microsoft.Extensions.Configuration;
using Novell.Directory.Ldap;
using PasswordKeeper.Application.Authorization;

namespace PasswordKeeper.Infrastructure.Authorization;

public class ActiveDirectoryService : IActiveDirectoryService
{
    private readonly string _ldapHost;
    private readonly int _ldapPort;
    private readonly string _domain;

    public ActiveDirectoryService(IConfiguration configuration)
    {
        _ldapHost = configuration["Ldap:Host"]!;
        _ldapPort = int.Parse(configuration["Ldap:Port"]!);
        _domain = configuration["ActiveDirectory:Domain"]!;
    }

    public async Task Login(string username, string password, CancellationToken cancellationToken = default)
    {
        try
        {
            using var connection = new LdapConnection();
            await connection.ConnectAsync(_ldapHost, _ldapPort, cancellationToken);
            await connection.BindAsync($"{username}@{_domain}", password, cancellationToken);
        }
        catch (LdapException ex)
        {
            // Invalid credentials
            if (ex.ResultCode == LdapException.InvalidCredentials)
            {
                throw new UnauthorizedAccessException($"Invalid credentials to {_domain}", ex);
            }

            throw;
        }
    }

    public Task Logout()
    {
        return Task.CompletedTask;
    }
}