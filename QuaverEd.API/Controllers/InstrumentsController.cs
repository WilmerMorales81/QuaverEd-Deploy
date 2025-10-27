using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

namespace QuaverEd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentsController : ControllerBase
    {
        private readonly QuaverEdContext _context;

        public InstrumentsController(QuaverEdContext context)
        {
            _context = context;
        }

        // GET: api/instruments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Instrument>>> GetInstruments()
        {
            try
            {
                var instruments = await _context.Instruments.ToListAsync();
                return Ok(instruments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving instruments", error = ex.Message });
            }
        }

        // Endpoint 7: Get all instruments that have sold in a given period
        [HttpGet("sold")]
        public async Task<ActionResult> GetSoldInstruments(DateTime from, DateTime to)
        {
            try
            {
                // Convert date to UTC para PostgreSQL
                var fromUtc = DateTime.SpecifyKind(from, DateTimeKind.Utc);
                var toUtc = DateTime.SpecifyKind(to, DateTimeKind.Utc);
                
                var soldInstruments = await _context.OrderItems
                    .Include(oi => oi.Instrument)
                    .Include(oi => oi.Order)
                    .Where(oi => oi.Order.OrderDate >= fromUtc && oi.Order.OrderDate <= toUtc)
                    .GroupBy(oi => new
                    {
                        oi.Instrument.Id,
                        oi.Instrument.Name,
                        oi.Instrument.ModelNumber,
                        oi.Instrument.Manufacturer,
                        oi.Instrument.Category
                    })
                    .Select(g => new
                    {
                        Instrument = new
                        {
                            g.Key.Id,
                            g.Key.Name,
                            g.Key.ModelNumber,
                            g.Key.Manufacturer,
                            g.Key.Category
                        },
                        TotalQuantitySold = g.Sum(oi => oi.Quantity),
                        TotalRevenue = g.Sum(oi => oi.TotalPrice),
                        AveragePrice = g.Average(oi => oi.UnitPrice),
                        OrdersCount = g.Select(oi => oi.OrderId).Distinct().Count()
                    })
                    .OrderByDescending(x => x.TotalQuantitySold)
                    .ToListAsync();

                return Ok(new
                {
                    message = $"Instruments that have sold in the period {from:yyyy-MM-dd} and {to:yyyy-MM-dd}",
                    period = new { from = from.ToString("yyyy-MM-dd"), to = to.ToString("yyyy-MM-dd") },
                    instrumentsCount = soldInstruments.Count,
                    totalRevenue = soldInstruments.Sum(x => x.TotalRevenue),
                    instruments = soldInstruments
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error to get sold instruments", error = ex.Message });
            }
        }
    }
}
