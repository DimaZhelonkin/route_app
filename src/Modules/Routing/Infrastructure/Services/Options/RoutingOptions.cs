using Ark.Routing.HttpClients.VkClient.Models.RequestModels;
using Ark.Routing.HttpClients.VkClient.Models.RequestModels.Enums;

namespace Ark.Routing.Services.Options;

public class RoutingOptions
{
    public DistanceUnits Units = DistanceUnits.Kilometers;
    public Language Language = Language.Russian;
    public DirectionsType DirectionsType = DirectionsType.Instructions;
    public ResponseCompleteness Completeness = ResponseCompleteness.Enriched;
// TODO why it names as DateTime if it is not a DateTime? Maybe you want to add some comments to props for understanding what exactly each property means?
    public List<RoutingDateTime> DateTime = new()
    {
        new RoutingDateTime
        {
            Type = RoutingDateTimeType.Default
        }
    };
        
    public Dictionary<CostingTransportType, CostingOptions> CostingOptions = new()
    {
        {
            CostingTransportType.Auto,
            new AutoCostingOptions()
            {
                Traffic = true,
            }
        }
    };
}