using Microsoft.AspNetCore.Mvc;
using PasswordKeeper.Application.Authorization;

namespace PasswordKeeper.WebApi.Authorization;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/v1/auth");

        group.MapPost("/login", Login)
            .WithName(nameof(Login))
            .AllowAnonymous();

        group.MapPost("/logout", Logout)
            .WithName(nameof(Logout))
            .RequireAuthorization();
    }

    public static async Task<IResult> Login(
        [FromBody] LoginRequest request,
        IAuthorizationService authorizationService,
        CancellationToken cancellationToken
    )
    {
        var token = await authorizationService.Login(request.Username, request.Password, cancellationToken);
        return Results.Ok(token);
    }

    public static async Task<IResult> Logout(
        IAuthorizationService authorizationService,
        CancellationToken cancellationToken
    )
    {
        await authorizationService.Logout(cancellationToken);
        return Results.Ok();
    }
}