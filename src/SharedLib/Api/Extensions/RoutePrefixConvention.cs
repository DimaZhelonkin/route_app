﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Ark.SharedLib.Api.Extensions;

public class RoutePrefixConvention : IApplicationModelConvention
{
    private readonly AttributeRouteModel _routePrefix;

    public RoutePrefixConvention(IRouteTemplateProvider route)
    {
        _routePrefix = new AttributeRouteModel(route);
    }

    #region IApplicationModelConvention Members

    public void Apply(ApplicationModel application)
    {
        foreach (var selector in application.Controllers.SelectMany(c => c.Selectors))
            selector.AttributeRouteModel = selector.AttributeRouteModel != null
                ? AttributeRouteModel.CombineAttributeRouteModel(_routePrefix, selector.AttributeRouteModel)
                : _routePrefix;
    }

    #endregion
}