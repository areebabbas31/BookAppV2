using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }  // Reference to the user who placed the order

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }  // Total amount of the order


        public string OrderStatus { get; set; } = "Completed";  // Status like "Pending", "Completed", "Shipped"

        // Navigation Property to the related OrderProducts
        public List<OrderProduct> OrderProducts { get; set; }

        // Optionally, you can add other fields like shipping address, payment method, etc.
    }

}
