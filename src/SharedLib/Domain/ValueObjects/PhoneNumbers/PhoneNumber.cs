using Ark.SharedLib.Common.Results;
using Ark.SharedLib.Common.Results.Extensions;

namespace Ark.SharedLib.Domain.ValueObjects.PhoneNumbers;

public class PhoneNumber : ValueObject
{
    public const int MaxLength = 32;

    private PhoneNumber(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static implicit operator PhoneNumber(string phoneNumber) => Create(phoneNumber).ThrowIfFailure().Value!;
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    public override string ToString() => Value;

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public static Result<PhoneNumber> Create(string phoneNumber) =>
        // TODO validate
        Result.Success(new PhoneNumber(phoneNumber));
}