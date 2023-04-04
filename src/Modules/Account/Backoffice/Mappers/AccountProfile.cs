using Ark.Account.Features.Account.RegisterCommand;
using Ark.Account.Features.Account.RegisterRequest;
using AutoMapper;

namespace Ark.Account.Mappers;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegisterRequest, RegisterCommand>();
    }
}