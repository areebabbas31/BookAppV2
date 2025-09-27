using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class CartProduct
    {

       
        public int CartId { get; set; }  // Foreign key to Cart
        public int ProductId { get; set; }  // Foreign key to Product
        public int Quantity { get; set; }  // Additional field for quantity

        // Navigation properties
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }


}



