namespace Ark.SharedLib.Common.Exceptions;

[Serializable]
public class InvalidSmartEnumPropertyName : ApplicationException
{
    public InvalidSmartEnumPropertyName(string property, string enumVal)
        : base($"The value `{enumVal}` is not valid for property `{property}`.")
    {
    }
}