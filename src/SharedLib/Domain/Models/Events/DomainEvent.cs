﻿using System.Reflection;
using Ark.SharedLib.Domain.Interfaces;

namespace Ark.SharedLib.Domain.Models.Events;

public abstract class DomainEvent : IDomainEvent, IEquatable<DomainEvent>
{
    private List<FieldInfo>? _fields;
    private List<PropertyInfo>? _properties;

    protected DomainEvent()
    {
        EventId = Guid.NewGuid();
    }

    #region IDomainEvent Members

    public Guid EventId { get; }

    #endregion

    #region IEquatable<DomainEvent> Members

    public bool Equals(DomainEvent? obj) => Equals(obj as object);

    #endregion

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
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

    private static int HashValue(int seed, object? value)
    {
        var currentHash = value?.GetHashCode() ?? 0;
        return seed * 23 + currentHash;
    }

    #region Nested type: IgnoreMemberAttribute

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    protected class IgnoreMemberAttribute : Attribute
    {
    }

    #endregion
}

public abstract class DomainEvent<TDomainEvent, TAggregateId> : DomainEvent<TAggregateId>
    where TDomainEvent : DomainEvent<TDomainEvent, TAggregateId>
{
    protected DomainEvent()
    {
    }

    protected DomainEvent(TAggregateId aggregateId) : base(aggregateId)
    {
    }

    protected DomainEvent(TAggregateId aggregateId, long aggregateVersion) : base(aggregateId, aggregateVersion)
    {
    }

    public override abstract TDomainEvent WithAggregate(TAggregateId aggregateId, long aggregateVersion);
}

public abstract class DomainEvent<TAggregateId> : DomainEvent, IDomainEvent<TAggregateId>
{
    protected DomainEvent()
    {
    }

    protected DomainEvent(TAggregateId aggregateId)
    {
        AggregateId = aggregateId;
    }

    protected DomainEvent(TAggregateId aggregateId, long aggregateVersion) : this(aggregateId)
    {
        AggregateVersion = aggregateVersion;
    }

    #region IDomainEvent<TAggregateId> Members

    public TAggregateId AggregateId { get; set; } // TODO delete костыль

    public long AggregateVersion { get; }

    #endregion

    // #region IEquatable<DomainEvent<TAggregateId>> Members
    //
    // public bool Equals(DomainEvent<TAggregateId>? other)
    // {
    //     return other != null &&
    //            EventId.Equals(other.EventId);
    // }
    //
    // #endregion
    //
    // public override bool Equals(object? obj)
    // {
    //     return Equals(obj as DomainEvent<TAggregateId>);
    // }
    //
    // public override int GetHashCode()
    // {
    //     return 290933282 + EqualityComparer<Guid>.Default.GetHashCode(EventId);
    // }

    public abstract IDomainEvent<TAggregateId> WithAggregate(TAggregateId aggregateId, long aggregateVersion);
}