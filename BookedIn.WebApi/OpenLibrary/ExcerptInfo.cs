using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record ExcerptInfo(
    [property: JsonPropertyName("excerpt")] string Excerpt,
    [property: JsonPropertyName("comment")] string Comment,
    [property: JsonPropertyName("author")] AuthorDetails Author
);