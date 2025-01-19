using System.Text.Json;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Domain.OpenLibraryExtensions;
using BookedIn.WebApi.OpenLibrary;

namespace BookedIn.WebApi.Books;

public class BookService(HttpClient httpClient) : IBookService
{
    public async Task<BookDetails?> GetBookDetailsByIdAsync(string id)
    {
        var response = await httpClient.GetAsync($"https://openlibrary.org/works/{id}.json");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var openLibraryBookDetails = JsonSerializer.Deserialize<OpenLibraryBookDetails>(jsonResponse);

        if (openLibraryBookDetails == null)
        {
            return null;
        }

        var authorDetailsTasks = openLibraryBookDetails.Authors.Select(async authorInfo =>
        {
            var authorResponse = await httpClient.GetAsync($"https://openlibrary.org{authorInfo.Author.Key}.json");
            authorResponse.EnsureSuccessStatusCode();
            
            var authorJsonResponse = await authorResponse.Content.ReadAsStringAsync();
            
            return JsonSerializer.Deserialize<OpenLibraryAuthor>(authorJsonResponse);
        });

        var openLibraryAuthors = await Task.WhenAll(authorDetailsTasks);

        return openLibraryBookDetails.ToBookDetails(openLibraryAuthors!);
    }

    public string GetCoverImageUrl(int coverId, string size)
    {
        return $"https://covers.openlibrary.org/b/id/{coverId}-{size.ToUpper()}.jpg";
    }
}