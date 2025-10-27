using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

namespace QuaverEd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly QuaverEdContext _context;

        public OrdersController(QuaverEdContext context)
        {
            _context = context;
        }

        // Endpoint 2: Get all orders by date range
        [HttpGet("date-range")]
        public async Task<ActionResult> GetOrdersByDateRange(DateTime from, DateTime to)
        {
            try
            {
                // Convert date to UTC for PostgreSQL  
                var fromUtc = DateTime.SpecifyKind(from, DateTimeKind.Utc);
                var toUtc = DateTime.SpecifyKind(to, DateTimeKind.Utc);
                
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Instrument)
                    .Where(o => o.OrderDate >= fromUtc && o.OrderDate <= toUtc)
                    .Select(o => new
                    {
                        o.Id,
                        o.OrderDate,
                        o.ShipDate,
                        o.Status,
                        o.TotalAmount,
                        o.Notes,
                        Customer = new
                        {
                            o.Customer.Id,
                            o.Customer.Name,
                            o.Customer.Email
                        },
                        ItemsCount = o.OrderItems.Count,
                        Items = o.OrderItems.Select(oi => new
                        {
                            oi.Instrument.Name,
                            oi.Instrument.ModelNumber,
                            oi.Quantity,
                            oi.UnitPrice,
                            oi.TotalPrice
                        })
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = $"Orders found in the period {from:yyyy-MM-dd} and {to:yyyy-MM-dd}",
                    dateRange = new { from = from.ToString("yyyy-MM-dd"), to = to.ToString("yyyy-MM-dd") },
                    count = orders.Count,
                    orders = orders
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error to get orders by date range", error = ex.Message });
            }
        }

        // Endpoint 3: Get all orders by price range
        [HttpGet("price-range")]
        public async Task<ActionResult> GetOrdersByPriceRange(decimal min, decimal max)
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.OrderItems)
                        .ThenInclude(oi => oi.Instrument)
                    .Where(o => o.TotalAmount >= min && o.TotalAmount <= max)
                    .Select(o => new
                    {
                        o.Id,
                        o.OrderDate,
                        o.ShipDate,
                        o.Status,
                        o.TotalAmount,
                        o.Notes,
                        Customer = new
                        {
                            o.Customer.Id,
                            o.Customer.Name,
                            o.Customer.Email
                        },
                        ItemsCount = o.OrderItems.Count,
                        Items = o.OrderItems.Select(oi => new
                        {
                            oi.Instrument.Name,
                            oi.Instrument.ModelNumber,
                            oi.Quantity,
                            oi.UnitPrice,
                            oi.TotalPrice
                        })
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = $"Orders found in the price range ${min:F2} and ${max:F2}",
                    priceRange = new { min = min.ToString("F2"), max = max.ToString("F2") },
                    count = orders.Count,
                    orders = orders
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error to get orders by price range", error = ex.Message });
            }
        }
    }
}
