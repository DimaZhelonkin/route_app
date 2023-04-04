using AutoMapper;

namespace Ark.SharedLib.Domain.ValueObjects.Percentages.Mappings;

public class PercentageProfile : Profile
{
    public PercentageProfile()
    {
        CreateMap<decimal, Percent>()
            .ConstructUsing(value => new Percent(value));
        CreateMap<Percent, decimal>()
            .ConstructUsing(percent => percent.Value);
    }
}