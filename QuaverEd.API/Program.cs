using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

Console.WriteLine("=== QuaverEd API Starting ===");
Console.WriteLine($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
Console.WriteLine($"Port: {Environment.GetEnvironmentVariable("PORT")}");
Console.WriteLine($"Database URL: {Environment.GetEnvironmentVariable("DATABASE_URL") != null ? "Configured" : "Not configured"}");

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine("=== Builder Created ===");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("=== Services Added ===");

// Add Entity Framework - ONLY if DATABASE_URL is available
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    Console.WriteLine("=== Adding Database Context ===");
    builder.Services.AddDbContext<QuaverEdContext>(options =>
        options.UseNpgsql(databaseUrl));
}
else
{
    Console.WriteLine("=== No Database URL - Skipping DB Context ===");
}

var app = builder.Build();

Console.WriteLine("=== App Built ===");

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

Console.WriteLine("=== Swagger Configured ===");

// Only use HTTPS redirection in development
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

Console.WriteLine("=== Authorization Configured ===");

app.MapControllers();

Console.WriteLine("=== Controllers Mapped ===");

// Health check endpoint
app.MapGet("/", () => new { 
    message = "QuaverEd API is running!", 
    timestamp = DateTime.UtcNow,
    version = "1.0.0",
    databaseUrl = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DATABASE_URL")) ? "Configured" : "Not configured",
    environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown"
});

Console.WriteLine("=== Root Endpoint Mapped ===");

Console.WriteLine("=== Starting Application ===");
app.Run();