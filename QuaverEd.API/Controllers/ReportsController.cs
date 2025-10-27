using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

namespace QuaverEd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly QuaverEdContext _context;

        public ReportsController(QuaverEdContext context)
        {
            _context = context;
        }

        // Endpoint 6: Get all customers that have ordered a specif type of instrument
        [HttpGet("customers-by-instrument")]
        public async Task<ActionResult> GetCustomersByInstrumentType(string category)
        {
            try
            {
                var customers = await _context.Customers
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.OrderItems)
                            .ThenInclude(oi => oi.Instrument)
                    .Where(c => c.Orders.Any(o => o.OrderItems.Any(oi => oi.Instrument.Category == category)))
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Email,
                        c.Phone,
                        c.Address,
                        OrdersWithThisCategory = c.Orders
                            .Where(o => o.OrderItems.Any(oi => oi.Instrument.Category == category))
                            .Count(),
                        TotalSpentOnCategory = c.Orders
                            .SelectMany(o => o.OrderItems)
                            .Where(oi => oi.Instrument.Category == category)
                            .Sum(oi => oi.TotalPrice),
                        InstrumentsPurchased = c.Orders
                            .SelectMany(o => o.OrderItems)
                            .Where(oi => oi.Instrument.Category == category)
                            .Select(oi => new
                            {
                                oi.Instrument.Name,
                                oi.Instrument.ModelNumber,
                                oi.Quantity,
                                oi.UnitPrice,
                                oi.TotalPrice,
                                OrderDate = oi.Order.OrderDate
                            })
                            .ToList()
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = $"Customers that have bought instruments by category '{category}'",
                    category = category,
                    customersCount = customers.Count,
                    customers = customers
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error to get customers by instrument category", error = ex.Message });
            }
        }
    }
}
