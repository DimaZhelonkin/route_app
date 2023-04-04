using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Application.Identity;
using Ark.SharedLib.Common.Exceptions;
using Keycloak.AuthServices.Authorization;
using MediatR;

namespace Ark.IdentityServer.Application.Features.Workspaces;

public record DeleteWorkspaceCommand(Guid Id) : IRequest;

public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand>
{
    private readonly IIdentityService identityService;

    public DeleteWorkspaceCommandHandler(IIdentityService identityService)
    {
        // this.db = db;
        this.identityService = identityService;
    }

    #region IRequestHandler<DeleteWorkspaceCommand> Members

    public async Task Handle(
        DeleteWorkspaceCommand request,
        CancellationToken cancellationToken)
    {
        var authorized = await identityService.AuthorizeAsync(
            ProtectedResourcePolicy.From("workspaces", request.Id.ToString(), "read"));

        if (!authorized) throw new ForbiddenAccessException();
        // var workspace = new Workspace() { Id = request.Id };
        // this.db.Workspaces.Remove(workspace);
        // await this.db.SaveChangesAsync(cancellationToken);
    }

    #endregion
}