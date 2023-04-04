using System.Resources;
using ViennaNET.Validation.Validators;

namespace Ark.SharedLib.Common.Validation;

public abstract class BaseValidationRuleSetWithResourceManager<T> : BaseValidationRuleSet<T>
{
    /// <summary>
    /// </summary>
    protected BaseValidationRuleSetWithResourceManager()
    {
        ResourceManager = GetResourceManagerForMessages();
    }

    /// <summary>
    /// </summary>
    protected ResourceManager ResourceManager { get; init; }

    /// <summary>
    /// TODO refactor DRY???
    /// </summary>
    /// <returns></returns>
    protected virtual ResourceManager GetResourceManagerForMessages()
    {
        var assembly =  GetType().Assembly;
        var resourcePath = assembly.GetName().Name! + ".Resources." + typeof(T).Name;
        var resourceManager = new ResourceManager(resourcePath, assembly);
        return resourceManager;
    }
}