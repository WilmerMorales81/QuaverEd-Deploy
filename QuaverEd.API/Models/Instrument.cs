using System.ComponentModel.DataAnnotations;

namespace QuaverEd.API.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ModelNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
        public decimal RetailPrice { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be grater than 0")]
        public decimal WholesalePrice { get; set; }
        
        public int QuantityOnHand { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        // Navigation
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}