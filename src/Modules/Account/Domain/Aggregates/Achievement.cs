using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Account.Aggregates;

public class Achievement : AggregateRootBase<Guid>
{
    public Achievement(Guid id) : base(id)
    {
    }

    public string Name { get; set; }
    public string? IconUrl { get; set; }
    public string Description { get; set; }
}