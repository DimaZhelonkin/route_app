using Ark.Account.Features.Account.RegisterRequest;
using Ark.Infrastructure.Shared.Controllers;
using Ark.Infrastructure.Shared.Result.AspNetCore;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Account.Controllers;

[Route("[controller]/[action]")]
public class AccountController : ApiBaseController
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [TranslateResultToActionResult]
    [ProducesDefaultResponseType(typeof(Result))]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    public async Task<Result> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken) =>
        await Mediator.Send(request, cancellationToken);
}