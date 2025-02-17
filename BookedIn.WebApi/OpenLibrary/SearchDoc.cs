﻿using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record SearchDoc(
    [property: JsonPropertyName("author_name")] List<string> AuthorNames,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("isbn")] List<string> Isbn,
    [property: JsonPropertyName("cover_i")] int? CoverId,
    [property: JsonPropertyName("key")] string Key // Property to hold the work ID
);