using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record DateTimeValue(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] DateTime Value
);