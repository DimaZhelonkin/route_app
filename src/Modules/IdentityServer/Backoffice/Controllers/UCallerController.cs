using Ark.IdentityServer.Application.UCaller.Features.Commands.CheckHealth;
using Ark.IdentityServer.Application.UCaller.Features.Commands.CheckPhone;
using Ark.IdentityServer.Application.UCaller.Features.Commands.GetInfo;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitRepeat;
using Ark.Infrastructure.Shared.Controllers;
using Ark.Infrastructure.Shared.Result.AspNetCore;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ark.IdentityServer.Backoffice.Controllers;

/// <summary>
/// </summary>
[Route("[controller]")]
public class UCallerController : ApiBaseController
{
    /// <summary>
    ///     Init Call
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <example>
    /// </example>
    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(InitCallResponse), StatusCodes.Status200OK)]
    public Task<Result<InitCallResponse>> InitCall([FromBody] InitCallCommand command,
        CancellationToken cancellationToken) => Mediator.Send(command, cancellationToken);

    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(InitRepeatCommand), StatusCodes.Status200OK)]
    public Task<Result<InitRepeatResponse>> InitRepeat([FromBody] InitRepeatCommand command,
        CancellationToken cancellationToken) => Mediator.Send(command, cancellationToken);

    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(GetInfoCommand), StatusCodes.Status200OK)]
    public Task<Result<GetInfoResponse>> GetInfo([FromQuery] GetInfoCommand command,
        CancellationToken cancellationToken) => Mediator.Send(command, cancellationToken);


    [HttpPost("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(CheckPhoneCommand), StatusCodes.Status200OK)]
    public Task<Result<CheckPhoneResponse>> CheckPhone([FromQuery] CheckPhoneCommand command,
        CancellationToken cancellationToken) => Mediator.Send(command, cancellationToken);


    [HttpGet("[action]")]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(CheckHealthCommand), StatusCodes.Status200OK)]
    public Task<Result<CheckHealthResponse>>
        GetHealth(CheckHealthCommand command, CancellationToken cancellationToken) =>
        Mediator.Send(command, cancellationToken);
}