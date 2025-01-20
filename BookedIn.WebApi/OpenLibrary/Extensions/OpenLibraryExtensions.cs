namespace BookedIn.WebApi.OpenLibrary.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenLibrary(this IServiceCollection services)
    {
        return services
            .AddSingleton(_ => new Func<string, Description>(s => new Description("Unknown", s)))
            .AddSingleton(_ => new Func<string, BioDetails>(s => new BioDetails("Unknown", s)))
            ;
    }
}