using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Bulky.Models
{
    public class OrderProduct
    {

        public int OrderId { get; set; }  // Foreign key to Order
        
        public int ProductId { get; set; }  // Foreign key to Product

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }  // Quantity of the product in the order

        [Required]
        [Range(0, double.MaxValue)]
        public double PriceAtTimeOfOrder { get; set; }  // Price at the time of the order

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

