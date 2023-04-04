using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Ark.StronglyTypedIds;

public static class StronglyTypedIdHelper
{
    private static readonly ConcurrentDictionary<Type, Delegate> StronglyTypedIdFactories = new();

    public static Func<TValue, object> GetFactory<TValue>(Type stronglyTypedIdType)
        where TValue : notnull =>
        (Func<TValue, object>)StronglyTypedIdFactories.GetOrAdd(
            stronglyTypedIdType,
            CreateFactory<TValue>);

    private static Func<TValue, object> CreateFactory<TValue>(Type stronglyTypedIdType)
        where TValue : notnull
    {
        if (!IsStronglyTypedId(stronglyTypedIdType))
            throw new ArgumentException($"Type '{stronglyTypedIdType}' is not a strongly-typed id type",
                nameof(stronglyTypedIdType));

        var ctor = stronglyTypedIdType.GetConstructor(new[] {typeof(TValue)});
        if (ctor is null)
            throw new ArgumentException(
                $"Type '{stronglyTypedIdType}' doesn't have a constructor with one parameter of type '{typeof(TValue)}'",
                nameof(stronglyTypedIdType));

        var param = Expression.Parameter(typeof(TValue), "value");
        var body = Expression.New(ctor, param);
        var lambda = Expression.Lambda<Func<TValue, object>>(body, param);
        return lambda.Compile();
    }

    public static bool IsStronglyTypedId(Type type) => IsStronglyTypedId(type, out _);

    public static bool IsStronglyTypedId(Type type, out Type? idType)
    {
        if (type is null)
            throw new ArgumentNullException(nameof(type));

        var findBaseType = GetBaseType(type, typeof(StronglyTypedId<>));
        if (findBaseType is null)
        {
            idType = null;
            return false;
        }

        idType = findBaseType.GetGenericArguments()[0];
        return true;

        Type? GetBaseType(Type currentType, Type toFind)
        {
            // if (!IsSubclassOfRawGeneric(toFind,currentType)) return null;
            var current = currentType;
            while (current != null && current != typeof(object)) {
                var cur = current.IsGenericType ? current.GetGenericTypeDefinition() : current;
                if (toFind == cur) return current;
                current = current.BaseType;
            }
            return null;
        }
        static bool IsSubclassOfRawGeneric(Type generic, Type toCheck) {
            while (toCheck != null && toCheck != typeof(object)) {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) return true;
                toCheck = toCheck.BaseType;
            }
            return false;
        }
    }
}