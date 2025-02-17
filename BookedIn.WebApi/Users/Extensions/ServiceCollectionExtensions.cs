﻿namespace BookedIn.WebApi.Users.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<SignUpRequestValidator>();

        return services;
    }
}