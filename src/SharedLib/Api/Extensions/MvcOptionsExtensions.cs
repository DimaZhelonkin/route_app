﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Ark.SharedLib.Api.Extensions;

public static class MvcOptionsExtensions
{
    public static void UseRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute) =>
        opts.Conventions.Add(new RoutePrefixConvention(routeAttribute));

    public static void UseRoutePrefix(this MvcOptions opts, string prefix) =>
        opts.UseRoutePrefix(new RouteAttribute(prefix));
}