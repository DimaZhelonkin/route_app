using AutoMapper;

namespace Ark.SharedLib.Domain.ValueObjects.Emails.Mappings;

public class EmailProfile : Profile
{
    public EmailProfile()
    {
        CreateMap<string, Email>()
            .ConstructUsing(value => value);
        CreateMap<Email, string>()
            .ConstructUsing(email => email.Value);
    }
}