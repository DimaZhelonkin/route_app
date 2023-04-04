using System.Text;
using Ark.Infrastructure.Shared.Extensions;
using Ark.SharedLib.Common.Results;
using Microsoft.AspNetCore.Mvc;

namespace Ark.Infrastructure.Shared.Result.AspNetCore;

/// <summary>
///     Extensions to support converting Result to an ActionResult
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    ///     Convert a <see cref="Result{T}" /> to a <see cref="ActionResult" />
    /// </summary>
    /// <typeparam name="T">The value being returned</typeparam>
    /// <param name="controller">The controller this is called from</param>
    /// <param name="result">The Result to convert to an ActionResult</param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller) =>
        controller.ToActionResult(result);

    /// <summary>
    ///     Convert a <see cref="Result" /> to a <see cref="ActionResult" />
    /// </summary>
    /// <param name="controller">The controller this is called from</param>
    /// <param name="result">The Result to convert to an ActionResult</param>
    /// <returns></returns>
    public static IActionResult ToActionResult(this SharedLib.Common.Results.Result result,
        ControllerBase controller) =>
        controller.ToActionResult(result);

    /// <summary>
    ///     Convert a <see cref="Result" /> to a <see cref="ActionResult" />
    /// </summary>
    /// <param name="controller">The controller this is called from</param>
    /// <param name="value">The Result to convert to an ActionResult</param>
    /// <returns></returns>
    public static IActionResult? ToActionResult(this ControllerBase controller, object? value)
    {
        if (value is null) return null;
        var valueType = value.GetType();
        var method = valueType
                     .GetExtensionMethods(nameof(ToActionResult))
                     .SingleOrDefault(m => valueType.IsGenericType == m.IsGenericMethod);
        if (method is null)
            return new ObjectResult(value);
        if (valueType.IsGenericType)
            method = method.MakeGenericMethod(valueType.GetGenericArguments()[0]);
        return (IActionResult)method.Invoke(null, new[] {value, controller})!;
    }

    /// <summary>
    ///     Convert a <see cref="Result{T}" /> to a <see cref="ActionResult" />
    /// </summary>
    /// <typeparam name="T">The value being returned</typeparam>
    /// <param name="controller">The controller this is called from</param>
    /// <param name="result">The Result to convert to an ActionResult</param>
    /// <returns></returns>
    public static IActionResult ToActionResult<T>(this ControllerBase controller,
        Result<T> result)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok:
                return controller.Ok(result.GetValue());
            case ResultStatus.NotFound: return NotFoundEntity(controller, result);
            case ResultStatus.Unauthorized: return controller.Unauthorized();
            case ResultStatus.Forbidden: return controller.Forbid();
            case ResultStatus.Invalid: return BadRequest(controller, result);
            case ResultStatus.Error: return UnprocessableEntity(controller, result);
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }

    /// <summary>
    ///     Convert a <see cref="Result" /> to a <see cref="ActionResult" />
    /// </summary>
    /// <param name="controller">The controller this is called from</param>
    /// <param name="result">The Result to convert to an ActionResult</param>
    /// <returns></returns>
    public static IActionResult ToActionResult(this ControllerBase controller,
        SharedLib.Common.Results.Result result)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok: return controller.Ok();
            case ResultStatus.NotFound: return NotFoundEntity(controller, result);
            case ResultStatus.Unauthorized: return controller.Unauthorized();
            case ResultStatus.Forbidden: return controller.Forbid();
            case ResultStatus.Invalid: return BadRequest(controller, result);
            case ResultStatus.Error: return UnprocessableEntity(controller, result);
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }

    private static IActionResult BadRequest(ControllerBase controller, SharedLib.Common.Results.Result result)
    {
        foreach (var error in result.Errors)
            controller.ModelState.AddModelError(error.Identifier, error.ErrorMessage);

        return controller.BadRequest(controller.ModelState);
    }

    private static IActionResult UnprocessableEntity(ControllerBase controller, SharedLib.Common.Results.Result result)
    {
        var details = new StringBuilder("Next error(s) occured:");

        foreach (var error in result.Errors)
            details.Append("* ").Append(error).AppendLine();

        return controller.UnprocessableEntity(new ProblemDetails
        {
            Title = "Something went wrong.",
            Detail = details.ToString(),
        });
    }

    private static IActionResult NotFoundEntity(ControllerBase controller, SharedLib.Common.Results.Result result)
    {
        var details = new StringBuilder("Next error(s) occured:");

        if (result.Errors.Any())
        {
            foreach (var error in result.Errors) details.Append("* ").Append(error).AppendLine();

            return controller.NotFound(new ProblemDetails
            {
                Title = "Resource not found.",
                Detail = details.ToString(),
            });
        }

        return controller.NotFound();
    }
}