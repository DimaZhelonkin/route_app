using AutoMapper;

namespace Ark.SharedLib.Domain.ValueObjects.PhoneNumbers.Mappings;

public class PhoneNumberProfile : Profile
{
    public PhoneNumberProfile()
    {
        CreateMap<string, PhoneNumber>()
            .ConstructUsing(value => value);
        CreateMap<PhoneNumber, string>()
            .ConstructUsing(email => email.Value);
    }
}