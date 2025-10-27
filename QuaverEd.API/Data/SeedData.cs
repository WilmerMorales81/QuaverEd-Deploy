using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Models;

namespace QuaverEd.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new QuaverEdContext(
                serviceProvider.GetRequiredService<DbContextOptions<QuaverEdContext>>()))
            {
                // Verificar si ya hay datos
                if (context.Customers.Any())
                {
                    return; // La base de datos ya tiene datos
                }

                // Crear datos de prueba
                var customers = new List<Customer>
                {
                    new Customer
                    {
                        Name = "Juan Pérez",
                        Email = "juan.perez@email.com",
                        Address = "Calle 123, Ciudad",
                        Phone = "555-0123",
                        ContactDate = DateTime.Now.AddDays(-30),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Customer
                    {
                        Name = "María García",
                        Email = "maria.garcia@email.com",
                        Address = "Avenida 456, Ciudad",
                        Phone = "555-0456",
                        ContactDate = DateTime.Now.AddDays(-15),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Customer
                    {
                        Name = "Carlos López",
                        Email = "carlos.lopez@email.com",
                        Address = "Carrera 789, Ciudad",
                        Phone = "555-0789",
                        ContactDate = DateTime.Now.AddDays(-7),
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };

                context.Customers.AddRange(customers);
                context.SaveChanges();

                var instruments = new List<Instrument>
                {
                    new Instrument
                    {
                        ModelNumber = "FENDER-001",
                        Name = "Stratocaster Standard",
                        Manufacturer = "Fender",
                        Category = "guitar",
                        RetailPrice = 899.99m,
                        WholesalePrice = 450.00m,
                        QuantityOnHand = 15,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Instrument
                    {
                        ModelNumber = "GIBSON-002",
                        Name = "Les Paul Studio",
                        Manufacturer = "Gibson",
                        Category = "guitar",
                        RetailPrice = 1299.99m,
                        WholesalePrice = 650.00m,
                        QuantityOnHand = 8,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Instrument
                    {
                        ModelNumber = "YAMAHA-003",
                        Name = "YAS-280 Alto Saxophone",
                        Manufacturer = "Yamaha",
                        Category = "brass",
                        RetailPrice = 1299.99m,
                        WholesalePrice = 650.00m,
                        QuantityOnHand = 12,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };

                context.Instruments.AddRange(instruments);
                context.SaveChanges();

                var orders = new List<Order>
                {
                    new Order
                    {
                        CustomerId = 1,
                        OrderDate = DateTime.Now.AddDays(-20),
                        ShipDate = DateTime.Now.AddDays(-18),
                        Status = "shipped",
                        TotalAmount = 899.99m,
                        Notes = "Entrega urgente",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Order
                    {
                        CustomerId = 2,
                        OrderDate = DateTime.Now.AddDays(-10),
                        ShipDate = DateTime.Now.AddDays(-8),
                        Status = "shipped",
                        TotalAmount = 1299.99m,
                        Notes = "Guitarra de alta gama",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    },
                    new Order
                    {
                        CustomerId = 3,
                        OrderDate = DateTime.Now.AddDays(-5),
                        ShipDate = null,
                        Status = "pending",
                        TotalAmount = 799.99m,
                        Notes = "Saxofón para estudiante",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };

                context.Orders.AddRange(orders);
                context.SaveChanges();

                var orderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        OrderId = 1,
                        InstrumentId = 1,
                        Quantity = 1,
                        UnitPrice = 899.99m,
                        TotalPrice = 899.99m,
                        CreatedAt = DateTime.Now
                    },
                    new OrderItem
                    {
                        OrderId = 2,
                        InstrumentId = 2,
                        Quantity = 1,
                        UnitPrice = 1299.99m,
                        TotalPrice = 1299.99m,
                        CreatedAt = DateTime.Now
                    },
                    new OrderItem
                    {
                        OrderId = 3,
                        InstrumentId = 3,
                        Quantity = 1,
                        UnitPrice = 799.99m,
                        TotalPrice = 799.99m,
                        CreatedAt = DateTime.Now
                    }
                };

                context.OrderItems.AddRange(orderItems);
                context.SaveChanges();
            }
        }
    }
}
