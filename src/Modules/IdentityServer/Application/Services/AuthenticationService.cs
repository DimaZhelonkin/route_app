using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.DTOs.KeyCloak;
using Ark.IdentityServer.Application.Features.Authentication.ConfirmAuthorizationCode;
using Ark.IdentityServer.Application.Features.Authentication.Login;
using Ark.SharedLib.Common.Results;
using MediatR;

namespace Ark.IdentityServer.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly ISender _sender;

    public AuthenticationService(ISender sender)
    {
        _sender = sender;
    }

    #region IAuthenticationService Members

    public Task<Result<string>> Login(string login, string password, string? returnUrl, bool rememberMe = false,
        CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<Result<ApplicationKeyCloakBaseObject>> ConfirmAuthorizationCode(string phoneNumber, string code,
        CancellationToken cancellationToken = default)
    {
        return _sender.Send(new ConfirmAuthorizationCodeCommand(phoneNumber, code), cancellationToken);
    }

    public Task<Result<string>> Login(ulong phoneNumber, CancellationToken cancellationToken = default) =>
        _sender.Send(new LoginCommand(phoneNumber), cancellationToken);

    public Task<Result> Logout(CancellationToken cancellationToken) => throw new NotImplementedException();

    #endregion
}