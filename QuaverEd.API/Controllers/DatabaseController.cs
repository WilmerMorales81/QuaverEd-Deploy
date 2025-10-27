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
                // Ensure database is created
                await _context.Database.EnsureCreatedAsync();
                
                // Add some sample data if tables are empty
                if (!await _context.Customers.AnyAsync())
                {
                    var customers = new List<Customer>
                    {
                        new Customer { Name = "John Doe", Email = "john@example.com", Phone = "123-456-7890" },
                        new Customer { Name = "Jane Smith", Email = "jane@example.com", Phone = "098-765-4321" }
                    };
                    
                    _context.Customers.AddRange(customers);
                    await _context.SaveChangesAsync();
                }

                if (!await _context.Instruments.AnyAsync())
                {
                    var instruments = new List<Instrument>
                    {
                        new Instrument { Name = "Guitar", Brand = "Fender", Price = 599.99m, Stock = 10 },
                        new Instrument { Name = "Piano", Brand = "Yamaha", Price = 1299.99m, Stock = 5 }
                    };
                    
                    _context.Instruments.AddRange(instruments);
                    await _context.SaveChangesAsync();
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
    }
}
