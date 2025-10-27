using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Entity Framework
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? 
                      builder.Configuration.GetConnectionString("DefaultConnection");

// Debug logging
Console.WriteLine($"DATABASE_URL: {Environment.GetEnvironmentVariable("DATABASE_URL")}");
Console.WriteLine($"Connection String: {connectionString}");

builder.Services.AddDbContext<QuaverEdContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable Swagger in production for Railway deployment
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuaverEd API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

// Only use HTTPS redirection in development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

// Health check endpoint
app.MapGet("/", () => new { 
    message = "QuaverEd API is running!", 
    timestamp = DateTime.UtcNow,
    version = "1.0.0"
});

app.Run();
