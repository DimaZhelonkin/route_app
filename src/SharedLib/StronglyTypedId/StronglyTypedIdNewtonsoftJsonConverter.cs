using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Ark.StronglyTypedIds;

public class StronglyTypedIdNewtonsoftJsonConverter<TStronglyTypedId, TValue> : JsonConverter<TStronglyTypedId>
    where TStronglyTypedId : StronglyTypedId<TValue>
    where TValue : notnull
{
    public override TStronglyTypedId ReadJson(JsonReader reader, Type objectType, TStronglyTypedId existingValue,
        bool hasExistingValue, JsonSerializer serializer)
    {
        if (reader.TokenType is JsonToken.Null)
            return null;

        var value = serializer.Deserialize<TValue>(reader);
        var factory = StronglyTypedIdHelper.GetFactory<TValue>(objectType);
        return (TStronglyTypedId)factory(value);
    }

    public override void WriteJson(JsonWriter writer, TStronglyTypedId value, JsonSerializer serializer)
    {
        if (value is null)
            writer.WriteNull();
        else
            writer.WriteValue(value.Value);
    }
}

public class StronglyTypedIdNewtonsoftJsonConverter : JsonConverter
{
    private static readonly ConcurrentDictionary<Type, JsonConverter> Cache = new();

    public override bool CanConvert(Type objectType) => StronglyTypedIdHelper.IsStronglyTypedId(objectType);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var converter = GetConverter(objectType);
        return converter.ReadJson(reader, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is null)
            writer.WriteNull();
        else
        {
            var converter = GetConverter(value.GetType());
            converter.WriteJson(writer, value, serializer);
        }
    }

    private static JsonConverter GetConverter(Type objectType) => Cache.GetOrAdd(objectType, CreateConverter);

    private static JsonConverter CreateConverter(Type objectType)
    {
        if (!StronglyTypedIdHelper.IsStronglyTypedId(objectType, out var valueType))
            throw new InvalidOperationException($"Cannot create converter for '{objectType}'");

        var type = typeof(StronglyTypedIdNewtonsoftJsonConverter<,>).MakeGenericType(objectType, valueType);
        return (JsonConverter)Activator.CreateInstance(type);
    }
}