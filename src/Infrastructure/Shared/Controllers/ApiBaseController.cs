using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Infrastructure.Shared.Controllers;

[ApiController]
[Produces("application/json")]
// [ProducesErrorResponseType(typeof(Astrum.Core.Common.Results.Result))]
[ProducesResponseType(typeof(UnprocessableEntityResult), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ForbidResult), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public abstract class ApiBaseController : Controller
{
    private ISender? _mediator;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}