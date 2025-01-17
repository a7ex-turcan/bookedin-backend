using MongoDB.Driver;

namespace BookedIn.WebApi.Mongo.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(_ =>
        {
            var connectionString = configuration.GetConnectionString("MongoConnection");
            return new MongoClient(connectionString);
        });
        return services;
    }
}