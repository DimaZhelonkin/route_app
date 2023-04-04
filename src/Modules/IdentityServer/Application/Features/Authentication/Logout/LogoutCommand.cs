using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using Microsoft.Extensions.Logging;

namespace Ark.IdentityServer.Application.Features.Authentication.Logout;

public record LogoutCommand : CommandResult
{
}

public sealed class LogoutCommandHandler : CommandHandler<LogoutCommand>
{
    private readonly ILogger _logger;
    // private readonly IUserAuthenticationService _userAuthenticationService;

    public LogoutCommandHandler(
            ILoggerFactory loggerFactory)
        // IUserAuthenticationService userAuthenticationService)
    {
        // _userAuthenticationService = userAuthenticationService;
        _logger = loggerFactory.CreateLogger<LogoutCommandHandler>();
    }

    public override async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken = default) =>
        // await _userAuthenticationService.Logout(cancellationToken);
        Result.Success();
}