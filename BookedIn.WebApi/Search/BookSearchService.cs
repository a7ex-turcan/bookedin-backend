// File: BookedIn.WebApi/Services/BookSearchService.cs

using System.Text.Json;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Services;

namespace BookedIn.WebApi.Search;

public class BookSearchService(HttpClient httpClient) : IBookSearchService
{
    public async Task<List<Book>> SearchBooksAsync(string query)
    {
        var response = await httpClient.GetAsync($"https://openlibrary.org/search.json?q={query}");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var searchResult = JsonSerializer.Deserialize<OpenLibrarySearchResult>(jsonResponse);

        return searchResult?.Docs ?? new List<Book>();
    }
}

public class OpenLibrarySearchResult
{
    public List<Book> Docs { get; set; } = [];
}