using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Account.Features.Account.RegisterCommand;

public class RegisterCommandHandler : CommandHandler<RegisterCommand>
{
    public override Task<Result> Handle(RegisterCommand command, CancellationToken cancellationToken = default) => throw
        // TODO create ApplicationUser
        // TODO create Passenger
        // TODO create Passenger Account
        // TODO create other types
        // TODO validate full logic
        // if authentication via email verification code
        // TODO send validation link to user email
        // TODO return success response
        // if authentication via email and password
        // TODO return auth token or return success response
        // TODO if success user have to fill auth form with email and password and he'll be authorized (on profile page he must confirm his email with code or just via sent link)
        new NotImplementedException();
}