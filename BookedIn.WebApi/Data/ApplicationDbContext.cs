using Microsoft.EntityFrameworkCore;

namespace BookedIn.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define your DbSets here. For example:
        // public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}