

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }  // Primary Key
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        [ValidateNever]
        public ICollection<Cart> Carts { get; set; } 
    }

    
}

