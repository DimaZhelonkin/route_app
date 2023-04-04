using System.Globalization;
using System.Reflection;
using Ark.SharedLib.Application.Abstractions.Services;
using Ark.SharedLib.Application.Abstractions.Shared;
using Microsoft.Extensions.Localization;

namespace Ark.Infrastructure.Resources.Services;

public class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResources);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        _localizer = factory.Create("SharedResources", assemblyName.Name);
    }

    #region ILocalizationService Members

    public LocalizedString this[string key] => _localizer[key];

    public LocalizedString GetLocalizedString(string key) => _localizer[key];

    public LocalizedString GetCulturedLocalizedString(string key, string culture)
    {
        var cultureInfo = new CultureInfo(culture);
        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;
        return GetLocalizedString(key);
    }

    public string GetLocalizedString(string key, params string[] parameters) =>
        string.Format(_localizer[key], parameters);

    #endregion
}