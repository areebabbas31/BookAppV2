
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Bulky.Models
{
	public class Product
	{
		
        [Key]
        public int ProductId { get; set; }
        [Required]
        [Display(Name="Title")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author{ get; set; }
        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }
        public List<CartProduct> CartProducts { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }


    }
}

