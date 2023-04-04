using Ark.SharedLib.Domain.Exceptions;
using ViennaNET.Validation.Validators;

namespace Ark.SharedLib.Domain.Extensions;

public static class ValidationResultExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="validationResult"></param>
    /// <typeparam name="TException"></typeparam>
    /// <returns></returns>
    /// <exception cref="TException"></exception>
    public static ValidationResult ThrowIfIsNotValid<TException>(this ValidationResult validationResult)
        where TException : DomainException
    {
        if (validationResult.IsValid)
            return validationResult;

        var constructor = typeof(TException).GetConstructor(new[] {validationResult.GetType()})!;
        var exception = (TException)constructor.Invoke(new object[] {validationResult});
        // var exception = (TException)Activator.CreateInstance(typeof(TException),new object[] {validationResult})!;
        throw exception;
    }
}