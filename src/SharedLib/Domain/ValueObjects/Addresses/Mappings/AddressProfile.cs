using AutoMapper;

namespace Ark.SharedLib.Domain.ValueObjects.Addresses.Mappings;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<string, PostalCode>()
            .ConstructUsing(value => new PostalCode(value));
        CreateMap<PostalCode, string>()
            .ConstructUsing(postalCode => postalCode.Value);
    }
}