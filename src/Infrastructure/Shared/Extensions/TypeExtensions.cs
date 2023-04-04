using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ark.Infrastructure.Shared.Extensions;

public static class TypeExtensions
{
    /// <summary>
    ///     This Methode extends the System.Type-type to get all extended methods. It searches hereby in all assemblies which
    ///     are known by the current AppDomain.
    /// </summary>
    /// <returns>returns MethodInfo[] with the extended Method</returns>
    public static MethodInfo[] GetExtensionMethods(this Type t, string? methodName = null, bool isGenericMethod = false)
    {
        var assTypes = new List<Type>();

        foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            assTypes.AddRange(item.GetTypes());

        var query = assTypes
                    .Where(type => type is {IsSealed: true, IsGenericType: false, IsNested: false})
                    .SelectMany(type =>
                            type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic),
                        (type, method) => new {type, method}
                    )
                    .Where(t1 => t1.method.IsDefined(typeof(ExtensionAttribute), false))
                    .Where(t1 =>
                    {
                        var parameterType = t1.method.GetParameters()[0].ParameterType;
                        return (!t.IsGenericType && parameterType == t) ||
                               (t.IsGenericType && parameterType.IsGenericType &&
                                parameterType.GetGenericTypeDefinition() == t.GetGenericTypeDefinition());
                    })
                    .Select(t1 => t1.method)
                    .Where(t1 => methodName is null || t1.Name == methodName);
        return query.ToArray();
    }

    /// <summary>
    ///     Extends the System.Type-type to search for a given extended MethodeName.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName">Name of the Methode</param>
    /// <param name="isGenericMethod">Is generic method</param>
    /// <returns>the found Methode or null</returns>
    public static MethodInfo? GetExtensionMethod(this Type type, string methodName, bool isGenericMethod = false) =>
        type.GetExtensionMethods(methodName, isGenericMethod).FirstOrDefault();
}