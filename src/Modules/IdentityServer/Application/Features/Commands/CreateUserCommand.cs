using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Commands;

public record CreateUserCommand : CommandResult
{
    public CreateUserCommand(string username, string name, string email, string password, List<string>? roles)
    {
        Username = username;
        Name = name;
        Email = email;
        Password = password;
        Roles = roles;
    }

    public string Username { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }
    public List<string>? Roles { get; init; }
}