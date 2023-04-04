using System.Security.Claims;

namespace Ark.SharedLib.Application.Identity;

public interface ICurrentUserService
{
    ClaimsPrincipal? User { get; }
    string? UserId { get; }
    string? Email { get; }
    string? FirstName { get; }
    string? LastName { get; }
    string? Username { get; }
    string? ClientId { get; }
    bool IsMachine { get; }
}