﻿using System.Text.Json.Serialization;
using BookedIn.WebApi.OpenLibrary.Serialization;

namespace BookedIn.WebApi.OpenLibrary;

public record OpenLibraryBookDetails(
    [property: JsonPropertyName("description"), JsonConverter(typeof(DescriptionConverter))]
    Description? Description,
    [property: JsonPropertyName("links")] List<Link> Links,
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("covers")] List<int> Covers,
    [property: JsonPropertyName("subject_places")] List<string> SubjectPlaces,
    [property: JsonPropertyName("subject_people")] List<string> SubjectPeople,
    [property: JsonPropertyName("key")] string Key,
    [property: JsonPropertyName("authors")] List<AuthorInfo> Authors,
    [property: JsonPropertyName("excerpts")] List<ExcerptInfo> Excerpts,
    [property: JsonPropertyName("type")] KeyObject Type,
    [property: JsonPropertyName("subjects")] List<string> Subjects,
    [property: JsonPropertyName("subject_times")] List<string> SubjectTimes,
    [property: JsonPropertyName("latest_revision")] int LatestRevision,
    [property: JsonPropertyName("revision")] int Revision,
    [property: JsonPropertyName("created")] Created Created,
    [property: JsonPropertyName("last_modified")] LastModified LastModified
);

public record Description(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value
);

public record Link(
    [property: JsonPropertyName("title")] string Title,
    [property: JsonPropertyName("url")] string Url,
    [property: JsonPropertyName("type")] KeyObject Type
);

public record AuthorInfo(
    [property: JsonPropertyName("author")] AuthorDetails Author,
    [property: JsonPropertyName("type")] KeyObject Type
);

public record AuthorDetails(
    [property: JsonPropertyName("key")] string Key
);

public record ExcerptInfo(
    [property: JsonPropertyName("excerpt")] string Excerpt,
    [property: JsonPropertyName("comment")] string Comment,
    [property: JsonPropertyName("author")] AuthorDetails Author
);

public record Created(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value
);

public record LastModified(
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("value")] string Value
);