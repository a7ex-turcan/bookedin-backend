using System.Text.Json;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Search;

public class BookSearchService(HttpClient httpClient) : IBookSearchService
{
    public async Task<List<Book>> SearchBooksAsync(string query)
    {
        var response = await httpClient.GetAsync($"https://openlibrary.org/search.json?q={query}");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var searchResult = JsonSerializer.Deserialize<OpenLibrarySearchResult>(jsonResponse);

        return searchResult?.Docs.Select(
                       doc => new Book(
                           Author: string.Join(", ", (doc.AuthorNames ?? [])),
                           Title: doc.Title,
                           Isbn: (doc.Isbn ?? []) .FirstOrDefault() ?? string.Empty,
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
}