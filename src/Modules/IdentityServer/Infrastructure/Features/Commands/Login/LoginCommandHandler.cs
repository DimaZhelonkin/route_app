using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.Features.Authentication.Login;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;
using Ark.IdentityServer.Domain.ApplicationUser.Specifications;
using Ark.IdentityServer.DomainServices.Repositories;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Ark.IdentityServer.Infrastructure.Features.Commands.Login;

public class LoginCommandHandler : CommandHandler<LoginCommand, string>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IDistributedCache _distributedCache;
    private readonly IPhoneAuthService _phoneAuthService;

    public LoginCommandHandler(IApplicationUserRepository applicationUserRepository, IPhoneAuthService phoneAuthService,
        IDistributedCache distributedCache)
    {
        _applicationUserRepository = applicationUserRepository;
        _phoneAuthService = phoneAuthService;
        _distributedCache = distributedCache;
    }

    public override async Task<Result<string>> Handle(LoginCommand command,
        CancellationToken cancellationToken = default)
    {
        // TODO ulong to string and to custom PhoneNumber valueObject where we can get ulong value of number
        var spec = new GetApplicationUserByPhoneNumberSpec(command.PhoneNumber.ToString());
        var applicationUser = await _applicationUserRepository.FirstOrDefaultAsync(spec, cancellationToken);
        if (applicationUser is null)
            return Result.NotFound<string>("LoginCommand", $"User not found with phone number: {command.PhoneNumber}");

        var key = $"InitCallLogin_{command.PhoneNumber}";
        var cacheString = await _distributedCache.GetStringAsync(key, cancellationToken);

        if (cacheString is not null)
            return Result.Error<string>("LoginCommand", "Call already initiated");

        var initCallResult =
            await _phoneAuthService.InitCall(new InitCallCommand(command.PhoneNumber), cancellationToken);
        if (initCallResult.IsFailure)
            return initCallResult.Void<string>();
        var initCallResponse = initCallResult.Value;

        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(initCallResponse),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
            },
            cancellationToken);

        return Result.Success(initCallResult.Value!.Code);
    }
}