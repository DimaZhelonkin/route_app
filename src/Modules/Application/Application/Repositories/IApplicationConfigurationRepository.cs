using Ark.Application.Aggregates;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Application.Repositories;

public interface IApplicationConfigurationRepository : IEntityRepository<ApplicationConfiguration, string>
{
}