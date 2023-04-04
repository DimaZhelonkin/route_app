using System.ComponentModel.DataAnnotations;
using Ark.SharedLib.Application.Abstractions.Services;

namespace Ark.SharedLib.Api.Validators.LocalizedDataAnnotationsValidator.Internals;

internal class LocalizedValidationContext
{
    internal LocalizedValidationContext(ILocalizationService localizer, object instance)
    {
        Localizer = localizer;
        Context = new ValidationContext(instance);
    }

    internal LocalizedValidationContext(ILocalizationService localizer, ValidationContext context)
    {
        Localizer = localizer;
        Context = context;
    }

    internal ILocalizationService Localizer { get; }

    internal ValidationContext Context { get; }
}