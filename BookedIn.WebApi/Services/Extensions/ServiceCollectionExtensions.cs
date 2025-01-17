using Microsoft.Extensions.DependencyInjection;

namespace BookedIn.WebApi.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserBookFavouriteService(this IServiceCollection services)
    {
        services.AddSingleton<IUserBookFavouriteService, UserBookFavouriteService>();
        return services;
    }
}