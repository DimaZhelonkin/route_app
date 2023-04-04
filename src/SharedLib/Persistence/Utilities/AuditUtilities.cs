using Ark.SharedLib.Persistence.Attributes;

namespace Ark.SharedLib.Persistence.Utilities;

static internal class AuditUtilities
{
    public static bool IsAuditDisabled(Type type)
    {
        var customAttributes = type.GetCustomAttributes(false);

        return IsAuditDisabled(customAttributes);
    }

    public static bool IsAuditDisabled(Type type, string propertyName)
    {
        if (propertyName == "Discriminator") //set Discriminator shadow property as non auditable
            return false;

        var props = type.GetProperties();
        var propsWithName = type.GetProperties().Where(x => x.Name == propertyName).ToList();
        var customAttributes = type.GetProperties()
                                   .SingleOrDefault(x => x.Name == propertyName && x.DeclaringType == type)
                                   ?.GetCustomAttributes(false) ?? Array.Empty<object>();
        return IsAuditDisabled(customAttributes);
    }

    private static bool IsAuditDisabled(object[] attributes)
    {
        foreach (var attribute in attributes)
            if (attribute is NotAuditableAttribute notAuditableAttribute)
                return notAuditableAttribute.Enabled;

        return false;
    }
}