using System.Text.Json;
using BookedIn.WebApi.Domain;
using BookedIn.WebApi.Domain.OpenLibraryExtensions;
using BookedIn.WebApi.OpenLibrary;
using BookedIn.WebApi.Auth;
using StackExchange.Redis;

namespace BookedIn.WebApi.Books;

public class BookService(
    HttpClient httpClient,
    ICurrentUserService currentUserService,
    IUserBookFavouriteService userBookFavouriteService,
    IConnectionMultiplexer redis
) : IBookService
{
    private readonly IDatabase _redisDb = redis.GetDatabase();
    
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

        var authorDetailsTasks = openLibraryBookDetails.Authors.Select(
            async authorInfo =>
            {
                var authorResponse = await httpClient.GetAsync($"https://openlibrary.org{authorInfo.Author.Key}.json");
                authorResponse.EnsureSuccessStatusCode();

                var authorJsonResponse = await authorResponse.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<OpenLibraryAuthor>(authorJsonResponse);
            }
        );

        var openLibraryAuthors = await Task.WhenAll(authorDetailsTasks);

        return openLibraryBookDetails.ToBookDetails(
            openLibraryAuthors!,
            await IsBookFavouriteAsync(id)
        );
    }

    public string GetCoverImageUrl(int coverId, string size)
    {
        return $"https://covers.openlibrary.org/b/id/{coverId}-{size.ToUpper()}.jpg";
    }
    
    public async Task<byte[]> GetCoverImageAsync(int coverId, string size)
    {
        var cacheKey = $"coverImage:{coverId}:{size}";
        var cachedImage = await _redisDb.StringGetAsync(cacheKey);

        if (cachedImage.HasValue)
        {
            return (byte[])cachedImage!;
        }

        var imageUrl = GetCoverImageUrl(coverId, size);
        var response = await httpClient.GetAsync(imageUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Failed to fetch the cover image.");
        }

        var imageData = await response.Content.ReadAsByteArrayAsync();
        await _redisDb.StringSetAsync(cacheKey, imageData);

        return imageData;
    }

    private async Task<bool> IsBookFavouriteAsync(string bookId)
    {
        var email = currentUserService.GetUserEmail();
        if (email == null)
        {
            return false;
        }

        return await userBookFavouriteService.GetByUserEmailAndWorkIdAsync(email, bookId) != null;
    }
}