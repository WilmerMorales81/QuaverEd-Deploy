using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;
using QuaverEd.API.Models;

namespace QuaverEd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly QuaverEdContext _context;

        public DatabaseController(QuaverEdContext context)
        {
            _context = context;
        }

        [HttpPost("init")]
        public async Task<ActionResult> InitializeDatabase()
        {
            try
            {
                // Try building connection string from individual variables
                var pgHost = Environment.GetEnvironmentVariable("PGHOST");
                var pgPort = Environment.GetEnvironmentVariable("PGPORT");
                var pgUser = Environment.GetEnvironmentVariable("PGUSER");
                var pgPassword = Environment.GetEnvironmentVariable("PGPASSWORD");
                var pgDatabase = Environment.GetEnvironmentVariable("PGDATABASE");
                
                Console.WriteLine($"=== Individual Variables Debug ===");
                Console.WriteLine($"PGHOST: '{pgHost}'");
                Console.WriteLine($"PGPORT: '{pgPort}'");
                Console.WriteLine($"PGUSER: '{pgUser}'");
                Console.WriteLine($"PGPASSWORD: '{(string.IsNullOrEmpty(pgPassword) ? "NULL" : "SET")}'");
                Console.WriteLine($"PGDATABASE: '{pgDatabase}'");
                
                // Build connection string manually
                var connectionString = $"Host={pgHost};Port={pgPort};Database={pgDatabase};Username={pgUser};Password={pgPassword};";
                Console.WriteLine($"=== Built Connection String ===");
                Console.WriteLine($"Connection: '{connectionString}'");
                
                // Create a temporary context with manually built connection string
                var optionsBuilder = new DbContextOptionsBuilder<QuaverEdContext>();
                optionsBuilder.UseNpgsql(connectionString);
                
                using var tempContext = new QuaverEdContext(optionsBuilder.Options);
                
                // Step 3: Create tables using migrations
                Console.WriteLine("=== Creating tables using migrations ===");
                await tempContext.Database.MigrateAsync();
                Console.WriteLine("=== Tables created successfully ===");
                
                // Add some sample data if tables are empty
                if (!await tempContext.Customers.AnyAsync())
                {
                    var customers = new List<Customer>
                    {
                        new Customer { Name = "John Doe", Email = "john@example.com", Phone = "123-456-7890" },
                        new Customer { Name = "Jane Smith", Email = "jane@example.com", Phone = "098-765-4321" }
                    };
                    
                    tempContext.Customers.AddRange(customers);
                    await tempContext.SaveChangesAsync();
                }

                if (!await tempContext.Instruments.AnyAsync())
                {
                    var instruments = new List<Instrument>
                    {
                        new Instrument { 
                            Name = "Electric Guitar", 
                            ModelNumber = "STRAT-001",
                            Manufacturer = "Fender", 
                            Category = "String",
                            RetailPrice = 599.99m,
                            WholesalePrice = 399.99m,
                            QuantityOnHand = 10,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        },
                        new Instrument { 
                            Name = "Digital Piano", 
                            ModelNumber = "P-125",
                            Manufacturer = "Yamaha", 
                            Category = "Keyboard",
                            RetailPrice = 1299.99m,
                            WholesalePrice = 899.99m,
                            QuantityOnHand = 5,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                    };
                    
                    tempContext.Instruments.AddRange(instruments);
                    await tempContext.SaveChangesAsync();
                }

                return Ok(new { message = "Database initialized successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error initializing database", error = ex.Message });
            }
        }

        [HttpGet("status")]
        public async Task<ActionResult> GetDatabaseStatus()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var customerCount = await _context.Customers.CountAsync();
                var instrumentCount = await _context.Instruments.CountAsync();
                
                return Ok(new { 
                    canConnect = canConnect,
                    customerCount = customerCount,
                    instrumentCount = instrumentCount,
                    message = "Database status retrieved successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error checking database status", error = ex.Message });
            }
        }

        [HttpGet("env")]
        public ActionResult GetEnvironmentVariables()
        {
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var aspnetcoreUrls = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
            var aspnetcoreEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var port = Environment.GetEnvironmentVariable("PORT");
            
            return Ok(new {
                DATABASE_URL = databaseUrl,
                DATABASE_URL_LENGTH = databaseUrl?.Length ?? 0,
                ASPNETCORE_URLS = aspnetcoreUrls,
                ASPNETCORE_ENVIRONMENT = aspnetcoreEnv,
                PORT = port,
                message = "Environment variables retrieved"
            });
        }
    }
}
