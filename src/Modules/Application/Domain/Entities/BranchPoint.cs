using Ark.Application.Enums;
using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Application.Entities;

public class BranchPoint : RemovableEntity<int>
{
    public BranchPoint(int id) : base(id)
    {
    }

    /// <summary>
    ///     Branch point indicative name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     FK for the Event Entity
    /// </summary>
    public Guid EventId { get; set; }

    /// <summary>
    ///     The type of the branch point
    /// </summary>
    public BranchPointTypeEnum Type { get; set; }

    /// <summary>
    ///     Navigation property of the event
    /// </summary>
    public virtual Event Event { get; set; }

    public virtual ICollection<RetroactiveEvent> RetroactiveEvents { get; set; }
}