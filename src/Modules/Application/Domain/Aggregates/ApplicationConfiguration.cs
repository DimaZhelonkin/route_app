using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Application.Aggregates;

/// <summary>
///     Aggregate root for application-specific configurations
/// </summary>
public class ApplicationConfiguration : AggregateRootBase<string>
{
    public ApplicationConfiguration(string id, string value, string? description = null, bool isEncrypted = false) :
        base(id)
    {
        Value = value;
        Description = description;
        IsEncrypted = isEncrypted;
    }

    /// <summary>
    ///     Description of the configuration variable.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Value of the configuration variable.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    ///     Indicates whether the <see cref="Value" /> field contains an encrypted text.
    /// </summary>
    public bool IsEncrypted { get; set; }
}