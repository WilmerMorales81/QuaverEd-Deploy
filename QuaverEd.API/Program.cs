using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

Console.WriteLine("=== QuaverEd API Starting ===");

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("=== Builder Created ===");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("=== Services Added ===");

// Add Entity Framework - ONLY if DATABASE_URL is available
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
Console.WriteLine($"=== DATABASE_URL Debug ===");
Console.WriteLine($"DATABASE_URL: '{databaseUrl}'");
Console.WriteLine($"Length: {databaseUrl?.Length ?? 0}");
Console.WriteLine($"Is null: {databaseUrl == null}");
Console.WriteLine($"Is empty: {string.IsNullOrEmpty(databaseUrl)}");

if (!string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine("=== Adding Database Context ===");
    try
    {
        builder.Services.AddDbContext<QuaverEdContext>(options =>
            options.UseNpgsql(databaseUrl));
        Console.WriteLine("=== Database Context Added Successfully ===");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"=== Error adding Database Context: {ex.Message} ===");
        throw;
    }
}
else
{
    Console.WriteLine("=== No Database URL - Skipping DB Context ===");
}

var app = builder.Build();

Console.WriteLine("=== App Built ===");

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuaverEd API V1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

Console.WriteLine("=== Swagger Configured ===");

app.UseAuthorization();

Console.WriteLine("=== Authorization Configured ===");

app.MapControllers();

Console.WriteLine("=== Controllers Mapped ===");

// Health check endpoint
app.MapGet("/", () => new { 
    message = "QuaverEd API is running!", 
    timestamp = DateTime.UtcNow,
    version = "1.0.0"
});

Console.WriteLine("=== Root Endpoint Mapped ===");

Console.WriteLine("=== Starting Application ===");

// Ensure we listen on the correct port
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
Console.WriteLine($"=== Listening on port: {port} ===");

app.Run($"http://0.0.0.0:{port}");