using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record OpenLibrarySearchResult(
    [property: JsonPropertyName("docs")] List<SearchDoc> Docs
);