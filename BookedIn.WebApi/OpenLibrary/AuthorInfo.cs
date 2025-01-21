using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record AuthorInfo(
    [property: JsonPropertyName("author")] AuthorDetails Author,
    [property: JsonPropertyName("type")] KeyObject Type
);