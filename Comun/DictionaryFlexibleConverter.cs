using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Formats.Asn1;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DictionaryFlexibleConverter<TValue> : Newtonsoft.Json.JsonConverter<Dictionary<string, TValue>>
{
    public override Dictionary<string, TValue> ReadJson(
        JsonReader reader,
        Type objectType,
        Dictionary<string, TValue> existingValue,
        bool hasExistingValue,
        Newtonsoft.Json.JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return new Dictionary<string, TValue>();

        var token = JToken.Load(reader);

        if (token.Type == JTokenType.Object)
            return token.ToObject<Dictionary<string, TValue>>(serializer)
                   ?? new Dictionary<string, TValue>();

        if (token.Type == JTokenType.Array)
            return new Dictionary<string, TValue>();

        return new Dictionary<string, TValue>();
    }

    public override void WriteJson(
        JsonWriter writer,
        Dictionary<string, TValue> value,
        Newtonsoft.Json.JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}