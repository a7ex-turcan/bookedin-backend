
namespace BookedIn.WebApi.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        // Register the password hashing service
        services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();


        return services;
    }
}