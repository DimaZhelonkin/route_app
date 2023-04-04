using Ark.SharedLib.Application.Abstractions.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Ark.SharedLib.Api.Validators.LocalizedDataAnnotationsValidator;

public class LocalizedDataAnnotationsValidator : ComponentBase
{
    [Inject]
    private ILocalizationService Localizer { get; set; }

    [CascadingParameter]
    private EditContext CurrentEditContext { get; set; }

    protected override void OnInitialized() => CurrentEditContext.AddLocalizedDataAnnotationsValidation(Localizer);
}