using System.Text.Json;
using BookedIn.WebApi.Search.OpenLibrary;

namespace BookedIn.WebApi.Books;

public interface IBookService
{
    Task<OpenLibraryBookDetails?> GetBookDetailsByIdAsync(string id);
    string GetCoverImageUrl(int coverId, string size);
}

public class BookService(HttpClient httpClient) : IBookService
{
    public async Task<OpenLibraryBookDetails?> GetBookDetailsByIdAsync(string id)
    {
        var response = await httpClient.GetAsync($"https://openlibrary.org/works/{id}.json");
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<OpenLibraryBookDetails>(jsonResponse);
    }

    public string GetCoverImageUrl(int coverId, string size)
    {
        return $"https://covers.openlibrary.org/b/id/{coverId}-{size.ToUpper()}.jpg";
    }
    
}