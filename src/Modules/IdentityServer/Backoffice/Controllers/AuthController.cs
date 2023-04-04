using Ark.IdentityServer.Application.DTOs.KeyCloak;
using Ark.IdentityServer.Application.Features.Authentication.ConfirmAuthorizationCode;
using Ark.IdentityServer.Application.Features.Authentication.Login;
using Ark.IdentityServer.Application.Features.Authentication.Logout;
using Ark.IdentityServer.Application.Features.Authentication.Register;
using Ark.IdentityServer.Application.Features.Commands.ChangePassword;
using Ark.Infrastructure.Shared.Controllers;
using Ark.Infrastructure.Shared.Result.AspNetCore;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ark.IdentityServer.Backoffice.Controllers;

/// <summary>
/// </summary>
[Route("[controller]")]
public class AuthController : ApiBaseController
{
    /// <summary>
    ///     Authentication via username or password
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <example>
    /// </example>
    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public Task<Result<string>> Login([FromBody] LoginCommand command, CancellationToken cancellationToken) =>
        Mediator.Send(command, cancellationToken);

    /// <summary>
    ///     Authorization code confirmation
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <example>
    /// </example>
    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public Task<Result<ApplicationKeyCloakBaseObject>> ConfirmAuthorizationCode([FromBody] ConfirmAuthorizationCodeCommand command,
        CancellationToken cancellationToken)
    {
        return Mediator.Send(command, cancellationToken);
    }

    /// <summary>
    ///     Logout user
    /// </summary>
    /// <returns></returns>
    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public Task<Result> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken) =>
        Mediator.Send(command, cancellationToken);

    /// <summary>
    ///     Change password
    /// </summary>
    /// <param name="command">Request body</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("change-password")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public Task<Result> ChangePassword([FromBody] ChangePasswordCommand command,
        CancellationToken cancellationToken)
        => Mediator.Send(command, cancellationToken);

    /// <summary>
    ///     Registration
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public Task<Result> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken) =>
        Mediator.Send(command, cancellationToken);
}