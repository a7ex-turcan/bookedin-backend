using System.Text.Json;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.OpenLibrary;
using Microsoft.AspNetCore.WebUtilities;

namespace BookedIn.WebApi.Search;

public class OpenLibraryBookSearchService(HttpClient httpClient) : IBookSearchService
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
                           Authors: doc.AuthorNames?.ToList() ??
                           [
                           ],
                           Title: doc.Title,
                           CoverId: doc.CoverId ?? 0,
                           WorkId: doc.Key.Replace("/works/", "") // Extracting just the ID
                       )
                   )
                   .ToList()
               ??
               [
               ];
    }
}