using System.Resources;
using ViennaNET.Validation.Rules.FluentRule;

// ReSharper disable VirtualMemberCallInConstructor

namespace Ark.SharedLib.Common.Validation;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseFluentRuleWithResourceManager<T> : BaseFluentRule<T>
{
    /// <summary>
    /// 
    /// </summary>
    protected BaseFluentRuleWithResourceManager()
    {
        ResourceManager = GetResourceManagerForMessages();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="internalCode"></param>
    protected BaseFluentRuleWithResourceManager(string internalCode) : base(internalCode)
    {
        ResourceManager = GetResourceManagerForMessages();
    }

    /// <summary>
    /// 
    /// </summary>
    protected ResourceManager ResourceManager { get; init; }

    /// <summary>
    /// TODO refactor DRY???
    /// </summary>
    /// <returns></returns>
    protected virtual ResourceManager GetResourceManagerForMessages()
    {
        var assembly = GetType().Assembly;
        var resourcePath = assembly.GetName().Name! + ".Resources." + typeof(T).Name;
        var resourceManager = new ResourceManager(resourcePath, assembly);
        return resourceManager;
    }
}