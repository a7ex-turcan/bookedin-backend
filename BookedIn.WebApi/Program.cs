using BookedIn.WebApi.Auth.Extensions;
using BookedIn.WebApi.Books.Extensions;
using BookedIn.WebApi.Data;
using BookedIn.WebApi.Mongo.Extensions;
using BookedIn.WebApi.Search.Extensions;
using BookedIn.WebApi.Users.Extensions;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with PostgreSQL
builder.Services
    .AddDbContext<ApplicationDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
    .AddMongoClient(builder.Configuration)
    .AddBooks()
    .AddBookSearch()
    .AddUsers()
    .AddAuthenticationServices(builder.Configuration);

// Configure Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(
    _ =>
    {
        var configuration = ConfigurationOptions.Parse(
            builder.Configuration.GetConnectionString("RedisConnection")
            ?? throw new Exception("Your redis config is null"),
            true
        );
        return ConnectionMultiplexer.Connect(configuration);
    }
);

var app = builder.Build();

// Apply migrations at startup
app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();