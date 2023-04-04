using System.Reflection;

namespace Ark.SharedLib.Api.Validators.LocalizedDataAnnotationsValidator.Internals.Extensions;

static internal class ReflectionExtensions
{
    static internal bool IsPublic(this PropertyInfo p)
    {
        if (!(p.GetMethod != null) || !p.GetMethod.IsPublic)
        {
            if (p.SetMethod != null) return p.SetMethod.IsPublic;
            return false;
        }

        return true;
    }
}