using Ark.SharedLib.Common.CQS.Implementations;

namespace Ark.IdentityServer.Application.Features.Commands;

public record AddRolesCommand : CommandResult
{
    public AddRolesCommand(string username, List<string> roles)
    {
        Username = username;
        Roles = roles;
    }

    public string Username { get; init; }
    public List<string> Roles { get; init; }
}
//
// /// <summary>
// ///     Add roles to user command handler
// /// </summary>
// public class AddRolesCommandHandler : CommandResultHandler<AddRolesCommand>
// {
//     private readonly IApplicationUserService _applicationUserService;
//
//     public AddRolesCommandHandler(IApplicationUserService applicationUserService)
//     {
//         _applicationUserService = applicationUserService;
//     }
//
//     public override async Task<Result> Handle(AddRolesCommand command, CancellationToken cancellationToken = default)
//     {
//         var request = new RoleAssignmentRequestDto
//         (
//             command.Username,
//             command.Roles
//         );
//
//         return await _applicationUserService.AddRoles(request);
//     }
// }