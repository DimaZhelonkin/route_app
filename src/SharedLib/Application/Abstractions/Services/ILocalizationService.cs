using Microsoft.Extensions.Localization;

namespace Ark.SharedLib.Application.Abstractions.Services;

public interface ILocalizationService
{
    LocalizedString this[string key] { get; }

    LocalizedString GetLocalizedString(string key);
    LocalizedString GetCulturedLocalizedString(string key, string culture);

    string GetLocalizedString(string key, params string[] parameters);
}