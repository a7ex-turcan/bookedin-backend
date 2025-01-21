using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record KeyObject(
    [property: JsonPropertyName("key")] string Key
);