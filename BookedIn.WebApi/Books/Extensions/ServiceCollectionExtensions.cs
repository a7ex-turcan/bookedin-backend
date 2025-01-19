namespace BookedIn.WebApi.Books.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserBookFavouriteService(this IServiceCollection services)
    {
        services.AddSingleton<IUserBookFavouriteService, UserBookFavouriteService>();
        return services;
    }
}