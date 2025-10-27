using Microsoft.EntityFrameworkCore;
using QuaverEd.API.Models;

namespace QuaverEd.API.Data
{
    public class QuaverEdContext : DbContext
    {
        public QuaverEdContext(DbContextOptions<QuaverEdContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure table in snake_case
            modelBuilder.Entity<Customer>().ToTable("customers");
            modelBuilder.Entity<Instrument>().ToTable("instruments");
            modelBuilder.Entity<Order>().ToTable("orders");
            modelBuilder.Entity<OrderItem>().ToTable("order_items");

            // Configure relationships
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Instrument)
                .WithMany(i => i.OrderItems)
                .HasForeignKey(oi => oi.InstrumentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes to optimize searches
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderDate);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CustomerId);

            modelBuilder.Entity<Instrument>()
                .HasIndex(i => i.Category);

            modelBuilder.Entity<Instrument>()
                .HasIndex(i => i.Manufacturer);
        }
    }
}