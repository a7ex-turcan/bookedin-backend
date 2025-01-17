using BookedIn.WebApi.Auth.Extensions;
using Microsoft.EntityFrameworkCore;
using BookedIn.WebApi.Data;
using BookedIn.WebApi.Mongo.Extensions;
using BookedIn.WebApi.Search.Extensions;
using BookedIn.WebApi.Services.Extensions;

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
    .AddUserBookFavouriteService()
    .AddBookSearch()
    .AddAuthenticationServices(builder.Configuration);

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