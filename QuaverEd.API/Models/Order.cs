using System.ComponentModel.DataAnnotations;

namespace QuaverEd.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        [Required]
        public int CustomerId { get; set; }
        
        [Required]
        public DateTime OrderDate { get; set; }
        
        public DateTime? ShipDate { get; set; }
        
        public string? Notes { get; set; }
        
        [StringLength(20)]
        public string Status { get; set; } = "pending";
        
        public decimal? TotalAmount { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        // Navigation
        public Customer Customer { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}