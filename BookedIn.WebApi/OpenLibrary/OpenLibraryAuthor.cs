﻿using System.Text.Json.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record OpenLibraryAuthor(
    [property: JsonPropertyName("personal_name")] string PersonalName,
    [property: JsonPropertyName("key")] string Key,
    [property: JsonPropertyName("entity_type")] string EntityType,
    [property: JsonPropertyName("birth_date")] string BirthDate,
    [property: JsonPropertyName("links")] List<Link> Links,
    [property: JsonPropertyName("alternate_names")] List<string> AlternateNames,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("remote_ids")] RemoteIds RemoteIds,
    [property: JsonPropertyName("type")] KeyObject Type,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("bio")] string Bio,
    [property: JsonPropertyName("fuller_name")] string FullerName,
    [property: JsonPropertyName("source_records")] List<string> SourceRecords,
    [property: JsonPropertyName("photos")] List<int> Photos,
    [property: JsonPropertyName("latest_revision")] int LatestRevision,
    [property: JsonPropertyName("revision")] int Revision,
    [property: JsonPropertyName("created")] DateTimeValue Created,
    [property: JsonPropertyName("last_modified")] DateTimeValue LastModified
);

public record RemoteIds(
    [property: JsonPropertyName("viaf")] string Viaf,
    [property: JsonPropertyName("goodreads")] string Goodreads,
    [property: JsonPropertyName("storygraph")] string Storygraph,
    [property: JsonPropertyName("isni")] string Isni,
    [property: JsonPropertyName("librarything")] string LibraryThing,
    [property: JsonPropertyName("amazon")] string Amazon,
    [property: JsonPropertyName("wikidata")] string Wikidata
);

public record KeyObject(
    [property: JsonPropertyName("key")] string Key
);

public record DateTimeValue(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] DateTime Value
);