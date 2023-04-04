using System.Reflection;
using Ark.SharedLib.Domain.Extensions;

namespace Ark.SharedLib.Domain.ValueObjects;

// public abstract class ValueObject
// {
//     protected static bool EqualOperator(ValueObject left, ValueObject right)
//     {
//         if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;
//         return ReferenceEquals(left, null) || left.Equals(right);
//     }
//
//     protected static bool NotEqualOperator(ValueObject left, ValueObject right)
//     {
//         return !EqualOperator(left, right);
//     }
//
//     protected abstract IEnumerable<object> GetAtomicValues();
//
//     public override bool Equals(object? obj)
//     {
//         if (obj == null || obj.GetType() != GetType()) return false;
//
//         var other = (ValueObject)obj;
//         using var thisValues = GetAtomicValues().GetEnumerator();
//         using var otherValues = other.GetAtomicValues().GetEnumerator();
//         while (thisValues.MoveNext() && otherValues.MoveNext())
//         {
//             if (ReferenceEquals(thisValues.Current, null) ^
//                 ReferenceEquals(otherValues.Current, null))
//                 return false;
//
//             if (thisValues.Current != null &&
//                 !thisValues.Current.Equals(otherValues.Current))
//                 return false;
//         }
//
//         return !thisValues.MoveNext() && !otherValues.MoveNext();
//     }
//
//     public override int GetHashCode()
//     {
//         var values = GetAtomicValues();
//         var hash = EnumerableExtensions.GetHashCode(values.Select(GetObjectHashCode));
//         return hash;
//     }
//
//     private int GetObjectHashCode(object? obj)
//     {
//         return obj?.GetHashCode() ?? 0;
//     }
// }

public abstract class ValueObject : IEquatable<ValueObject>
{
    private List<FieldInfo>? _fields;
    private List<PropertyInfo>? _properties;

    #region IEquatable<ValueObject> Members

    public bool Equals(ValueObject? obj) => Equals(obj as object);

    #endregion

    public static bool operator ==(ValueObject? obj1, ValueObject? obj2)
    {
        if (Equals(obj1, null))
        {
            if (Equals(obj2, null))
                return true;

            return false;
        }

        return obj1.Equals(obj2);
    }

    protected bool AtomicValuesEquals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (ValueObject)obj;
        using var thisValues = GetAtomicValues().GetEnumerator();
        using var otherValues = other.GetAtomicValues().GetEnumerator();
        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (ReferenceEquals(thisValues.Current, null) ^
                ReferenceEquals(otherValues.Current, null))
                return false;

            if (thisValues.Current != null &&
                !thisValues.Current.Equals(otherValues.Current))
                return false;
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public static bool operator !=(ValueObject? obj1, ValueObject? obj2) => !(obj1 == obj2);

    protected virtual IEnumerable<object> GetAtomicValues()
    {
        yield break;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        if (GetAtomicValues().Any())
            return AtomicValuesEquals(obj);
        return GetProperties().All(p => PropertiesAreEqual(obj, p)) && GetFields().All(f => FieldsAreEqual(obj, f));
    }

    private bool PropertiesAreEqual(object obj, PropertyInfo p) =>
        Equals(p.GetValue(this, null), p.GetValue(obj, null));

    private bool FieldsAreEqual(object obj, FieldInfo f) => Equals(f.GetValue(this), f.GetValue(obj));

    private IEnumerable<PropertyInfo> GetProperties() =>
        _properties ??= GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                        .ToList();

    private IEnumerable<FieldInfo> GetFields() =>
        _fields ??= GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
                             .Where(p => p.GetCustomAttribute(typeof(IgnoreMemberAttribute)) == null)
                             .ToList();

    public override int GetHashCode()
    {
        unchecked //allow overflow
        {
            var values = GetAtomicValues().ToList();
            if (values.Any())
                return EnumerableExtensions.GetHashCode(values.Select(GetObjectHashCode));

            var hash = 17;
            foreach (var prop in GetProperties())
            {
                var value = prop.GetValue(this, null);
                hash = HashValue(hash, value);
            }

            foreach (var field in GetFields())
            {
                var value = field.GetValue(this);
                hash = HashValue(hash, value);
            }

            return hash;
        }
    }

    private int HashValue(int seed, object? value)
    {
        var currentHash = GetObjectHashCode(value);
        return seed * 23 + currentHash;
    }

    private int GetObjectHashCode(object? obj) => obj?.GetHashCode() ?? 0;
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class IgnoreMemberAttribute : Attribute
{
}