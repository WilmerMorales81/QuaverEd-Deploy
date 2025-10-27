using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuaverEd.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert customers
            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Name", "Address", "Phone", "Email", "ContactDate", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "John Smith", "123 Main Street, New York", "555-0101", "john.smith@email.com", new DateTime(2024, 1, 15), new DateTime(2024, 1, 15), new DateTime(2024, 1, 15) },
                    { "Sarah Johnson", "456 Central Avenue, Los Angeles", "555-0102", "sarah.johnson@email.com", new DateTime(2024, 2, 20), new DateTime(2024, 2, 20), new DateTime(2024, 2, 20) },
                    { "Michael Brown", "789 Oak Street, Chicago", "555-0103", "michael.brown@email.com", new DateTime(2024, 3, 10), new DateTime(2024, 3, 10), new DateTime(2024, 3, 10) },
                    { "Emily Davis", "321 Pine Road, Houston", "555-0104", "emily.davis@email.com", new DateTime(2024, 4, 5), new DateTime(2024, 4, 5), new DateTime(2024, 4, 5) },
                    { "David Wilson", "654 Elm Boulevard, Phoenix", "555-0105", "david.wilson@email.com", new DateTime(2024, 5, 12), new DateTime(2024, 5, 12), new DateTime(2024, 5, 12) }
                });

            // Insert instruments
            migrationBuilder.InsertData(
                table: "instruments",
                columns: new[] { "ModelNumber", "Name", "Manufacturer", "Category", "RetailPrice", "WholesalePrice", "QuantityOnHand", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { "FENDER-001", "Stratocaster Standard", "Fender", "guitar", 899.99m, 450.00m, 15, DateTime.Now, DateTime.Now },
                    { "GIBSON-002", "Les Paul Studio", "Gibson", "guitar", 1299.99m, 650.00m, 8, DateTime.Now, DateTime.Now },
                    { "YAMAHA-003", "YAS-280 Alto Saxophone", "Yamaha", "brass", 1299.99m, 650.00m, 12, DateTime.Now, DateTime.Now },
                    { "ROLAND-004", "TD-17KV Electronic Drum Kit", "Roland", "percussion", 799.99m, 400.00m, 6, DateTime.Now, DateTime.Now },
                    { "STEINWAY-005", "Model M Grand Piano", "Steinway", "piano", 89999.99m, 45000.00m, 2, DateTime.Now, DateTime.Now },
                    { "MARTIN-006", "D-28 Acoustic Guitar", "Martin", "guitar", 3299.99m, 1650.00m, 4, DateTime.Now, DateTime.Now },
                    { "TRUMPET-007", "Bach TR300 Trumpet", "Bach", "brass", 899.99m, 450.00m, 10, DateTime.Now, DateTime.Now },
                    { "PEARL-008", "Vision Birch Drum Kit", "Pearl", "percussion", 1299.99m, 650.00m, 7, DateTime.Now, DateTime.Now }
                });

            // Insert orders
            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "CustomerId", "OrderDate", "ShipDate", "Notes", "Status", "TotalAmount", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 20), new DateTime(2024, 1, 22), "Urgent delivery", "shipped", 899.99m, DateTime.Now, DateTime.Now },
                    { 2, new DateTime(2024, 2, 25), new DateTime(2024, 2, 27), "VIP customer", "shipped", 1299.99m, DateTime.Now, DateTime.Now },
                    { 3, new DateTime(2024, 3, 15), new DateTime(2024, 3, 17), "First purchase", "shipped", 799.99m, DateTime.Now, DateTime.Now },
                    { 1, new DateTime(2024, 4, 10), new DateTime(2024, 4, 12), "Second purchase", "shipped", 3299.99m, DateTime.Now, DateTime.Now },
                    { 4, new DateTime(2024, 5, 15), null, "Pending shipment", "paid", 899.99m, DateTime.Now, DateTime.Now },
                    { 5, new DateTime(2024, 6, 1), null, "Recent order", "pending", 1299.99m, DateTime.Now, DateTime.Now },
                    { 2, new DateTime(2024, 6, 5), null, "Frequent customer", "pending", 89999.99m, DateTime.Now, DateTime.Now }
                });

            // Insert order items
            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "OrderId", "InstrumentId", "Quantity", "UnitPrice", "TotalPrice", "CreatedAt" },
                values: new object[,]
                {
                    { 1, 1, 1, 899.99m, 899.99m, DateTime.Now },
                    { 2, 2, 1, 1299.99m, 1299.99m, DateTime.Now },
                    { 3, 4, 1, 799.99m, 799.99m, DateTime.Now },
                    { 4, 6, 1, 3299.99m, 3299.99m, DateTime.Now },
                    { 5, 7, 1, 899.99m, 899.99m, DateTime.Now },
                    { 6, 8, 1, 1299.99m, 1299.99m, DateTime.Now },
                    { 7, 5, 1, 89999.99m, 89999.99m, DateTime.Now }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
