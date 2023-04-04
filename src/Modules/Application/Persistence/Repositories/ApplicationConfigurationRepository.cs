using Ardalis.Specification;
using Ark.Application.Aggregates;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Application.Repositories;

internal class ApplicationConfigurationRepository :
    EFRepository<ApplicationConfiguration, string, ApplicationDbContext>,
    IApplicationConfigurationRepository
{
    public ApplicationConfigurationRepository(ApplicationDbContext context,
        ISpecificationEvaluator? specificationEvaluator = null) : base(context, specificationEvaluator)
    {
    }
}