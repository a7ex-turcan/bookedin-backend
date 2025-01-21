using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary.Serialization;

public class BioConverter : JsonConverter<BioDetails?>
{
    public override BioDetails? Read(
        ref Utf8JsonReader reader,
        System.Type typeToConvert,
        JsonSerializerOptions options
    ) =>
        reader.TokenType switch
        {
            JsonTokenType.String => new BioDetails("unknown", reader.GetString() ?? string.Empty),
            JsonTokenType.StartObject => JsonSerializer.Deserialize<BioDetails>(ref reader, options),
            _ => null
        };

    public override void Write(
        Utf8JsonWriter writer,
        BioDetails? value,
        JsonSerializerOptions options
    )
    {
        if (value != null)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}