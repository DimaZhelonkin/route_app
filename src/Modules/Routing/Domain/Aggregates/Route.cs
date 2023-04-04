using Ark.SharedLib.Domain.Models.Entities;
using NetTopologySuite.Geometries;

namespace Ark.Routing.Aggregates;

/// <summary>
///     TODO Was made for special storing
/// </summary>
public class Route : AggregateRootBase<Guid>
{
    private Route(Guid id) : base(id)
    {
    }

    public Route(Guid id, LineString route) : base(id)
    {
        Current = route;
    }

    public LineString Current { get; set; }
    // public Point Location { get; set; }

    public Point StartPoint => Current.StartPoint;
    public Point DestinationPoint => Current.EndPoint;
}