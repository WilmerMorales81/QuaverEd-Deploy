using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Data;

namespace QuaverEd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly QuaverEdContext _context;

        public CustomersController(QuaverEdContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customers", error = ex.Message });
            }
        }

        // Endpoint 1: Search customers by personal data
        [HttpGet("search")]
        public async Task<ActionResult> SearchCustomers(string? name, string? email, string? address)
        {
            try
            {
                var query = _context.Customers.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(c => EF.Functions.ILike(c.Name, $"%{name}%"));
                }

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(c => EF.Functions.ILike(c.Email, $"%{email}%"));
                }

                if (!string.IsNullOrEmpty(address))
                {
                    query = query.Where(c => c.Address != null && EF.Functions.ILike(c.Address, $"%{address}%"));
                }

                var customers = await query.ToListAsync();

                return Ok(new
                {
                    message = "Customer search completed",
                    count = customers.Count,
                    customers = customers.Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Email,
                        c.Phone,
                        c.Address,
                        c.ContactDate
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error searching customers", error = ex.Message });
            }
        }

        // Endpoint 4: Get all customers with order totals
        [HttpGet("summary")]
        public async Task<ActionResult> GetCustomersSummary()
        {
            try
            {
                var customersSummary = await _context.Customers
                    .Select(c => new
                    {
                        c.Id,
                        c.Name,
                        c.Email,
                        TotalOrders = c.Orders.Count,
                        TotalAmount = c.Orders.Sum(o => o.TotalAmount ?? 0)
                    })
                    .ToListAsync();

                return Ok(new
                {
                    message = "Customer summary retrieved successfully",
                    count = customersSummary.Count,
                    customers = customersSummary
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customer summary", error = ex.Message });
            }
        }

        // Endpoint 5: Get instruments purchased by a specific customer
        [HttpGet("{id}/instruments")]
        public async Task<ActionResult> GetCustomerInstruments(int id)
        {
            try
            {
                var customer = await _context.Customers
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.OrderItems)
                            .ThenInclude(oi => oi.Instrument)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (customer == null)
                {
                    return NotFound(new { message = $"Customer with ID {id} not found" });
                }

                var instruments = customer.Orders
                    .SelectMany(o => o.OrderItems)
                    .Select(oi => new
                    {
                        oi.Instrument.Id,
                        oi.Instrument.Name,
                        oi.Instrument.ModelNumber,
                        oi.Instrument.Manufacturer,
                        oi.Instrument.Category,
                        QuantityPurchased = oi.Quantity,
                        PurchasePrice = oi.UnitPrice,
                        OrderDate = oi.Order.OrderDate
                    })
                    .Distinct()
                    .ToList();

                return Ok(new
                {
                    message = $"Instruments purchased by {customer.Name}",
                    customer = new { customer.Id, customer.Name, customer.Email },
                    instrumentsCount = instruments.Count,
                    instruments = instruments
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customer instruments", error = ex.Message });
            }
        }
    }
}
