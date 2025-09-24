using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;

        }
    
        public IActionResult AddToCart(int productId)
        {

            var cartId = 2;  // Retrieve or create the CartId (for simplicity, using 1 here)

            // Retrieve or create a cart based on CartId
            var cart = _context.Carts.FirstOrDefault
                (c => c.CartId == cartId);


            if (cart == null)
            {
                // If no cart exists, create a new cart
                cart = new Cart();
                _context.Carts.Add(cart);
                Console.Write("hekki"+"hi"+cart.CartId+"hello");
                _context.SaveChanges();  // Use synchronous SaveChanges
            }

            // Check if the product is already in the cart
            var cartProduct = _context.CartProducts
                .FirstOrDefault(cp => cp.CartId == cart.CartId && cp.ProductId == productId);

            if (cartProduct != null)
            {
                // If product exists, increase quantity
                cartProduct.Quantity++;
            }
            else
            {
                var newCartProduct = new CartProduct
                {
                    Quantity = 1,
                    CartId = cart.CartId,
                    ProductId = productId
                };

                _context.CartProducts.Add(newCartProduct);
            }

            _context.SaveChanges();  // Use synchronous SaveChanges

            return RedirectToAction("Index", "Product");




        }

    }
}