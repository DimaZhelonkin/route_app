using Ark.IdentityServer.Application.Authorization;
using MediatR;

namespace Ark.IdentityServer.Application.Features.Workspaces;

[AuthorizeProtectedResource(
    "workspaces", "workspaces:delete",
    ResourceAuthorizationMode.ResourceFromRequest)]
public record DeleteWorkspaceCommandAOP(Guid Id) : IRequest, IRequestWithResourceId
{
    #region IRequestWithResourceId Members

    public string ResourceId => Id.ToString();

    #endregion
}

public class DeleteWorkspaceCommandAOPHandler : IRequestHandler<DeleteWorkspaceCommandAOP>
{
    #region IRequestHandler<DeleteWorkspaceCommandAOP> Members

    // private readonly IApplicationDbContext db;

    // public DeleteWorkspaceCommandAOPHandler(IApplicationDbContext db) =>
    // this.db = db;

    public async Task Handle(
        DeleteWorkspaceCommandAOP request,
        CancellationToken cancellationToken)
    {
        // var workspace = new Workspace() {Id = request.Id};
        // this.db.Workspaces.Remove(workspace);
        // await this.db.SaveChangesAsync(cancellationToken);
    }

    #endregion
}