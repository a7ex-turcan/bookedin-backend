using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Data;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var pendingMigrations = context.Database.GetPendingMigrations();

        if (pendingMigrations.Any()) context.Database.Migrate();
    }
}