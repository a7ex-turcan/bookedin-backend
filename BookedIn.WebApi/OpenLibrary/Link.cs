using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record Link(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("type")] KeyObject Type
);