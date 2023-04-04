using Ark.IdentityServer.Application.DTOs.KeyCloak;
using Ark.IdentityServer.Application.Features.Authentication.ConfirmAuthorizationCode;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.Contracts;

public interface IAuthenticationService
{
    Task<Result<string>> Login(string login, string password, string? returnUrl, bool rememberMe = false,
        CancellationToken cancellationToken = default);

    Task<Result<string>> Login(ulong phoneNumber, CancellationToken cancellationToken = default);

    Task<Result<ApplicationKeyCloakBaseObject>> ConfirmAuthorizationCode(string phoneNumber, string code,
        CancellationToken cancellationToken = default);

    Task<Result> Logout(CancellationToken cancellationToken);
}