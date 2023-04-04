using Ark.Infrastructure.Shared.Controllers;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Api.Controllers;

[Route("[controller]")]
public class AntiForgeryController : ApiBaseController
{
    private readonly IAntiforgery _antiforgery;

    public AntiForgeryController(IAntiforgery antiforgery, IHttpContextAccessor httpContextAccessor)
    {
        _antiforgery = antiforgery;
    }

    // [HttpGet]
    // public IActionResult GetToken()
    // {
    //     _antiforgery.SetCookieTokenAndHeader(HttpContext);
    //     return Ok();
    // }
}