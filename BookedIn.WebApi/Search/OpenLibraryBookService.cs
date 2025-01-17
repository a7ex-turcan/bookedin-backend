using System.Text.Json;
using System.Threading;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search.OpenLibrary;
using Microsoft.AspNetCore.WebUtilities;

namespace BookedIn.WebApi.Search;

public class OpenLibraryBookService(HttpClient httpClient) : IBookService
{
    public async Task<List<Book>> SearchBooksAsync(
        string query,
        int? limit,
        CancellationToken cancellationToken
    )
    {
        var queryParams = new Dictionary<string, string>
        {
            ["q"] = query.Replace(' ', '+')
        };

        if (limit.HasValue)
        {
            queryParams["limit"] = limit.Value.ToString();
        }

        var requestUri = QueryHelpers.AddQueryString("https://openlibrary.org/search.json", queryParams!);
        var response = await httpClient.GetAsync(requestUri, cancellationToken);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var searchResult = JsonSerializer.Deserialize<OpenLibrarySearchResult>(jsonResponse);

        return searchResult?.Docs.Select(
                       doc => new Book(
                           Author: string.Join(", ", (doc.AuthorNames ?? [])),
                           Title: doc.Title,
                           CoverId: doc.CoverId ?? 0,
                           WorkId: doc.Key.Replace("/works/", "") // Extracting just the ID
                       )
                   )
                   .ToList()
               ?? new List<Book>();
    }

    public async Task<OpenApiBookDetails?> GetBookDetailsByIdAsync(string id)
    {
        var response = await httpClient.GetAsync($"https://openlibrary.org/works/{id}.json");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OpenApiBookDetails>(jsonResponse);
    }

    public string GetCoverImageUrl(int coverId, string size)
    {
        return $"https://covers.openlibrary.org/b/id/{coverId}-{size}.jpg";
    }
}