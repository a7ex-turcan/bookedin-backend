using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record LastModified(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value
);