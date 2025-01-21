using System.Text.Json.Serialization;
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