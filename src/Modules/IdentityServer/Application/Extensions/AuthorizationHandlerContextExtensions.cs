﻿using Microsoft.AspNetCore.Authorization;

namespace Ark.IdentityServer.Application.Extensions;

public static class AuthorizationHandlerContextExtensions
{
    public static void EvaluateRequirement(this AuthorizationHandlerContext context,
        IAuthorizationRequirement requirement, Func<bool> expression)
    {
        if (expression.Invoke())
            context.Succeed(requirement);
        else
            context.Fail();
    }
}