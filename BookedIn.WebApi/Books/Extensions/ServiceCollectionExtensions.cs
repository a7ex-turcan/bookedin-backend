using BookedIn.WebApi.Search;

namespace BookedIn.WebApi.Books.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBooks(this IServiceCollection services)
    {
        services.AddSingleton<IUserBookFavouriteService, UserBookFavouriteService>();
        services.AddSingleton<IBookService, BookService>();

        services.AddHttpClient<IBookService, BookService>();
        
        return services;
    }
}