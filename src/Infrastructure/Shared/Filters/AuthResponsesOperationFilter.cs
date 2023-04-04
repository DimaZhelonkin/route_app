using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ark.Infrastructure.Shared.Filters;

public class AuthResponsesOperationFilter : IOperationFilter
{
    #region IOperationFilter Members

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                                    .Union(context.MethodInfo.GetCustomAttributes(true))
                                    .OfType<AuthorizeAttribute>();

        if (authAttributes.Any())
            operation.Responses.Add("401", new OpenApiResponse {Description = "Unauthorized"});
    }

    #endregion
}