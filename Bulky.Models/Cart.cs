using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Cart
    {


        public int CartId { get; set; }
        public int UserId { get; set; }  // Foreign Key to User
        public User User { get; set; }  //
        

        public List<CartProduct> CartProducts { get; set; }
       
    
    }
}

