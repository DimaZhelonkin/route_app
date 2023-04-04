using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Commands.ChangePassword;

/// <summary>
///     Represent a change password command type
/// </summary>
public record ChangePasswordCommand : CommandResult
{
    /// <summary>
    ///     Field for user new password
    /// </summary>
    public string NewPassword { get; set; }

    /// <summary>
    ///     Field for confirm password
    /// </summary>
    public string ConfirmPassword { get; set; }
}