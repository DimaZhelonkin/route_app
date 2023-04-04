using System.Resources;
using Ark.SharedLib.Common.Exceptions;
using ViennaNET.Validation.Rules.ValidationResults;
using ViennaNET.Validation.Validators;

namespace Ark.SharedLib.Domain.Exceptions;

/// <summary>
///     Represent an abstract generic class for all domain exceptions
/// </summary>
public abstract class DomainException<TDomainEntity> : DomainException
{
    protected DomainException(string messageCode)
    {
        ResourceManager = GetResourceManagerFromCurrentAssembly();
        var message = ResourceManager.GetString(messageCode)!;
        Errors = new Dictionary<string, string[]>
        {
            [messageCode] = new[] {message},
        };
    }

    protected DomainException(string messageCode, ValidationResult validationResult) : base(
        GetResourceManagerFromCurrentAssembly().GetString(messageCode)!,
        validationResult)
    {
        ResourceManager = GetResourceManagerFromCurrentAssembly();
        var message = ResourceManager.GetString(messageCode)!;
        Errors = new Dictionary<string, string[]>
        {
            [messageCode] = new[] {message},
        };
    }

    /// <summary>
    /// </summary>
    protected ResourceManager ResourceManager { get; init; }

    private static ResourceManager GetResourceManagerFromCurrentAssembly()
    {
        var assembly = typeof(TDomainEntity).Assembly;
        var resourcePath = assembly.GetName().Name! + ".Resources." + typeof(TDomainEntity).Name;
        var resourceManager = new ResourceManager(resourcePath, assembly);
        return resourceManager;
    }
}

/// <summary>
///     Represent an abstract class for all domain exceptions
/// </summary>
public abstract class DomainException : ValidationException
{
    protected DomainException()
    {
    }

    protected DomainException(string message, ValidationResult validationResult)
        : base(string.Join(Environment.NewLine, message, validationResult.Results.ToErrorsString()),
            validationResult)
    {
    }
}