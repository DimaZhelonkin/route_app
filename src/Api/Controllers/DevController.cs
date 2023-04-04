using Ark.Infrastructure.Shared.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Api.Controllers;

[Authorize(Roles = "SuperAdmin")]
[Route("[controller]")]
public class DevController : ApiBaseController
{
}