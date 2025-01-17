using BookedIn.WebApi.Services;

namespace BookedIn.WebApi.Search.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBookSearch(this IServiceCollection services)
    {
        services.AddSingleton<IBookService, OpenLibraryBookService>();
        services.AddHttpClient<IBookService, OpenLibraryBookService>();
        
        return services;
    }
}