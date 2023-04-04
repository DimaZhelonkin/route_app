using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ark.Infrastructure.Shared.Result.AspNetCore;

public class TranslateResultToActionResultAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var contextResult = (context.Result as ObjectResult)?.Value;
        if (contextResult is not SharedLib.Common.Results.Result) return;
        if (context.Controller is not ControllerBase controller) return;

        context.Result = controller.ToActionResult(contextResult);
    }
}