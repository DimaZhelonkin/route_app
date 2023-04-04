﻿using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Application.Entities;

public class RetroactiveEvent : RemovableEntity<Guid>
{
    public RetroactiveEvent(Guid id) : base(id)
    {
    }

    /// <summary>
    ///     FK of the branch this event belongs to
    /// </summary>
    public int BranchPointId { get; set; }

    /// <summary>
    ///     The payload of the retroactive event
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    ///     The sequence of the retroactive event
    /// </summary>
    public int Sequence { get; set; }

    /// <summary>
    ///     The assembly type name of the retroactive event
    /// </summary>
    public string AssemblyTypeName { get; set; }

    /// <summary>
    ///     Indicates whether to ignore or apply this event
    /// </summary>
    public bool IsEnabled { get; set; }

    public virtual BranchPoint BranchPoint { get; set; }
}