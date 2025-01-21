using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record AuthorDetails(
    [property: JsonPropertyName("key")] string Key
);