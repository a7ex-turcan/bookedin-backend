using BookedIn.WebApi.Services;

namespace BookedIn.WebApi.Search.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookSearch(this IServiceCollection services)
    {
        services.AddSingleton<IBookSearchService, BookSearchService>();
        services.AddHttpClient<IBookSearchService, BookSearchService>();
        
        return services;
    }
}