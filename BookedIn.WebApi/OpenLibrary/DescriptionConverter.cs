using BookedIn.WebApi.Search.OpenLibrary;
using System.Text.Json;
using System.Text.Json.Serialization;
using Type = System.Type;

namespace BookedIn.WebApi.OpenLibrary;

public class DescriptionConverter : JsonConverter<Description?>
{
    public override Description? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    ) =>
        reader.TokenType switch
        {
            JsonTokenType.String => new Description("unknown", reader.GetString() ?? string.Empty),
            JsonTokenType.StartObject => JsonSerializer.Deserialize<Description>(ref reader, options),
            _ => null
        };

    public override void Write(
        Utf8JsonWriter writer,
        Description? value,
        JsonSerializerOptions options
    )
    {
        if (value != null)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}