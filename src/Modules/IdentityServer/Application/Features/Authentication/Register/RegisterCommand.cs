using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Authentication.Register;

/// <summary>
///     Represent register command
/// </summary>
public record RegisterCommand : CommandResult
{
    /// <summary>
    ///     Username field
    /// </summary>
    public string Username { get; init; }

    /// <summary>
    ///     First name field
    /// </summary>
    public string FirstName { get; init; }

    /// <summary>
    ///     Last name field
    /// </summary>
    public string LastName { get; init; }

    /// <summary>
    ///     Email field
    /// </summary>
    public string Email { get; init; }

    /// <summary>
    ///     Phone number field
    /// </summary>
    public string PhoneNumber { get; init; }

    /// <summary>
    ///     Date of birth
    /// </summary>
    public DateTimeOffset BirthDate { get; set; }

    /// <summary>
    ///     User password field
    /// </summary>
    // public string Password { get; init; }

    /// <summary>
    ///     User confirm password field
    /// </summary>
    // public string ConfirmPassword { get; init; }
}