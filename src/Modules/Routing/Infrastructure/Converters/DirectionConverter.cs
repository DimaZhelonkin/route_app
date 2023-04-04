using System.Text.Json;
using System.Text.Json.Serialization;
using Ark.Routing.HttpClients.VkClient.Models;
using Ark.Routing.HttpClients.VkClient.Models.ResponseModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace Ark.Routing.Converters;

public class DirectionConverter : Newtonsoft.Json.JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jo = JObject.Load(reader);
        var tripsArray = jo.SelectToken("trips");
        var listTrips = tripsArray?
            .Select(tripsProp => tripsProp.SelectToken("trip"))
            .Select(trp => trp?.ToObject(typeof(Trip), serializer) as Trip ?? null)
            .ToList();
        var directionResponseObject = jo.ToObject<DirectionsResponse>(new JsonSerializer());
        if (directionResponseObject != null)
        {
            directionResponseObject.Trips = listTrips ?? new List<Trip?>();
        }

        return directionResponseObject;
    }

    public override bool CanConvert(Type objectType)
    {
        
        return (objectType == typeof(DirectionsResponse));
    }
}