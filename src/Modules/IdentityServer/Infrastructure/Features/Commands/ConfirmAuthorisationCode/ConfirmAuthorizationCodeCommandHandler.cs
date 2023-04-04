using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.Features.Authentication.ConfirmAuthorizationCode;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;
using Ark.IdentityServer.Domain.ApplicationUser.Specifications;
using Ark.IdentityServer.DomainServices.Repositories;
using Ark.IdentityServer.Infrastructure.Keycloak;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Helpers;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ApplicationKeyCloakBaseObject = Ark.IdentityServer.Application.DTOs.KeyCloak.ApplicationKeyCloakBaseObject;

namespace Ark.IdentityServer.Infrastructure.Features.Commands.ConfirmAuthorisationCode;

public class ConfirmAuthorizationCodeCommandHandler : CommandHandler<ConfirmAuthorizationCodeCommand, ApplicationKeyCloakBaseObject>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly IDistributedCache _distributedCache;
    private readonly IKeyCloakAuthenticationService _keyCloakAuthenticationService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public ConfirmAuthorizationCodeCommandHandler(IApplicationUserRepository applicationUserRepository,
        IUserService userService,
        IKeyCloakAuthenticationService keyCloakAuthenticationService,
        IDistributedCache distributedCache,
        IMapper mapper)
    {
        _applicationUserRepository = applicationUserRepository;
        _userService = userService;
        _keyCloakAuthenticationService = keyCloakAuthenticationService;
        _distributedCache = distributedCache;
        _mapper = mapper;
    }

    public override async Task<Result<ApplicationKeyCloakBaseObject>> Handle(ConfirmAuthorizationCodeCommand command,
        CancellationToken cancellationToken = default)
    {
        var key = $"InitCallLogin_{command.PhoneNumber}"; // TODO to constant
        var cacheString = await _distributedCache.GetStringAsync(key, cancellationToken); 
        var cacheResult = CacheHelper<InitCallCommand,ApplicationKeyCloakBaseObject>
            .Create($"Authorization code for {command.PhoneNumber} is expired",
                $"Authorization code for {command.PhoneNumber} is expired" )
            .CheckCacheValidity(cacheString, command.PhoneNumber).Value!
            .CheckCacheEquals(
                x=> command.Code != x.Code,
                identifier:$"{command.PhoneNumber}:{command.Code}");
        if (cacheResult.Status is ResultStatus.Invalid or ResultStatus.Error) return cacheResult;

        // if (cacheString is null)
        // {
        //     var a = Result.Invalid<ApplicationKeyCloakBaseObject>(new Error(
        //     {
        //         Identifier = command.PhoneNumber,
        //         // Severity = ValidationSeverity.Error,
        //         // ErrorCode = "",
        //         ErrorMessage = $"Authorization code for {command.PhoneNumber} is expired"
        //     });
        //     return Result.Invalid<ApplicationKeyCloakBaseObject>(new Error(
        //     {
        //         Identifier = command.PhoneNumber,
        //         // Severity = ValidationSeverity.Error,
        //         // ErrorCode = "",
        //         ErrorMessage = $"Authorization code for {command.PhoneNumber} is expired"
        //     });
        // }

        // // var cachedResponse = JsonConvert.DeserializeObject<InitCallResponse>(cacheString);
        // // if (cachedResponse is null)
        // //     return Result.Error<ApplicationKeyCloakBaseObject>(new Error(
        // //     {
        // //         Identifier =command.PhoneNumber,
        // //         ErrorMessage = $"Authorization code for {command.PhoneNumber} is expired"
        // //     });
        // // if (command.Code != cachedResponse.Code)
        // //     return Result.Invalid<ApplicationKeyCloakBaseObject>(new Error.
        // //     {
        // //         Identifier = $"{command.PhoneNumber}:{command.Code}",
        // //         // Severity = ValidationSeverity.Error,
        // //         // ErrorCode = "",
        // //         ErrorMessage = $"Authorization code {command.Code} is not valid"
        // //     });
        //
        var spec = new GetApplicationUserByPhoneNumberSpec(command.PhoneNumber);
        var applicationUser = await _applicationUserRepository.FirstOrDefaultAsync(spec, cancellationToken);
        if (applicationUser is null)
            throw new Exception();
        var user = await _userService.GetUserByUsername(applicationUser.Username);
        if (user is null)
            throw new Exception();

        if (user.Enabled != true)
            throw new Exception("User is not active");

        var tokenResponse =  await _keyCloakAuthenticationService.Authenticate(applicationUser, cancellationToken);
        var mappedKeyCloakObject = _mapper.Map<ApplicationKeyCloakBaseObject>(tokenResponse);
        
        return Result.Success(mappedKeyCloakObject);
    }
}