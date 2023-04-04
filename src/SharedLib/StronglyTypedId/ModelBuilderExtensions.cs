using System.Collections.Concurrent;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ark.StronglyTypedIds;

/// <summary>
/// </summary>
public static class ModelBuilderExtensions
{
    private static readonly ConcurrentDictionary<Type, ValueConverter> StronglyTypedIdConverters = new();

    public static void AddStronglyTypedIdConversions(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
                if (StronglyTypedIdHelper.IsStronglyTypedId(property.ClrType, out var valueType))
                {
                    var converter = StronglyTypedIdConverters.GetOrAdd(
                        property.ClrType,
                        _ => CreateStronglyTypedIdConverter(property.ClrType, valueType));
                    property.SetValueConverter(converter);
                }
        }
    }

    private static ValueConverter CreateStronglyTypedIdConverter(
        Type stronglyTypedIdType,
        Type valueType)
    {
        // id => id.Value
        var toProviderFuncType = typeof(Func<,>)
            .MakeGenericType(stronglyTypedIdType, valueType);
        var stronglyTypedIdParam = Expression.Parameter(stronglyTypedIdType, "id");
        var toProviderExpression = Expression.Lambda(
            toProviderFuncType,
            Expression.Property(stronglyTypedIdParam, "Value"),
            stronglyTypedIdParam);

        // value => new ProductId(value)
        var fromProviderFuncType = typeof(Func<,>)
            .MakeGenericType(valueType, stronglyTypedIdType);
        var valueParam = Expression.Parameter(valueType, "value");
        var ctor = stronglyTypedIdType.GetConstructor(new[] {valueType});
        var fromProviderExpression = Expression.Lambda(
            fromProviderFuncType,
            Expression.New(ctor, valueParam),
            valueParam);

        var converterType = typeof(ValueConverter<,>)
            .MakeGenericType(stronglyTypedIdType, valueType);

        return (ValueConverter)Activator.CreateInstance(
            converterType,
            toProviderExpression,
            fromProviderExpression,
            null)!;
    }
}