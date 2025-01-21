using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record RemoteIds(
    [property: JsonPropertyName("viaf")] string Viaf,
    [property: JsonPropertyName("goodreads")]
    string Goodreads,
    [property: JsonPropertyName("storygraph")]
    string Storygraph,
    [property: JsonPropertyName("isni")] string Isni,
    [property: JsonPropertyName("librarything")]
    string LibraryThing,
    [property: JsonPropertyName("amazon")] string Amazon,
    [property: JsonPropertyName("wikidata")]
    string Wikidata
);