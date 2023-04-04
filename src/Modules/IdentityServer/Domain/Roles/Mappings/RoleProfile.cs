using AutoMapper;

namespace Ark.IdentityServer.Domain.Roles.Mappings;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<string, Role>()
            .ConstructUsing(value => new Role(value));
        CreateMap<Role, string>()
            .ConstructUsing(role => role.Value);
    }
}