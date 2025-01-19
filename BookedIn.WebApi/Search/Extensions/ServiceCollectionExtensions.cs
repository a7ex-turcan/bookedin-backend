namespace BookedIn.WebApi.Search.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookSearch(this IServiceCollection services)
    {
        services.AddSingleton<IBookSearchService, OpenLibraryBookSearchService>();
        services.AddHttpClient<IBookSearchService, OpenLibraryBookSearchService>();
        
        return services;
    }
}