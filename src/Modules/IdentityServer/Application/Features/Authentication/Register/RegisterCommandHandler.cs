using Ark.IdentityServer.Application.Extensions;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.IdentityServer.DomainServices.Repositories;
using Ark.SharedLib.Application.Abstractions.Services;
using Ark.SharedLib.Application.Abstractions.Shared;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Ark.IdentityServer.Application.Features.Authentication.Register;

public sealed class RegisterCommandHandler : CommandHandler<RegisterCommand>
{
    private readonly IApplicationUserRepository _applicationUserRepository;
    private readonly ILocalizationService _localizer;
    private readonly ILogger<RegisterCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordGenerator _passwordGenerator;
    private readonly IPasswordHasher _passwordHasher;

    /// <summary>
    ///     Represent register command handler
    /// </summary>
    /// <param name="mapper"></param>
    public RegisterCommandHandler(
        ILogger<RegisterCommandHandler> logger,
        IMapper mapper, IPasswordHasher passwordHasher,
        IApplicationUserRepository applicationUserRepository,
        IPasswordGenerator passwordGenerator)
    {
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _passwordGenerator = passwordGenerator;
        _applicationUserRepository = applicationUserRepository;
        _logger = logger;
    }

    public override async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        // TODO to fluent validation
        var newPassword = _passwordGenerator.GenerateRandomPassword();
        var hashedPassword = _passwordHasher.Hash(newPassword);
        var applicationUserId = new ApplicationUserId(Guid.NewGuid());
        var user = ApplicationUser.Create(applicationUserId,
            request.Username,
            request.Email,
            request.FirstName,
            request.LastName,
            request.PhoneNumber,
            newPassword,
            request.BirthDate);
        await _applicationUserRepository.AddAsync(user, cancellationToken);
        await _applicationUserRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}