using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulkyBookWeb.Data;
using Microsoft.EntityFrameworkCore;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Packaging.Signing;

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

           HttpContext.Session.SetInt32("UserId", 1); // Retrieve or create the CartId (for simplicity, using 1 here)
            int? userId = HttpContext.Session.GetInt32("UserId");
            // Retrieve or create a cart based on CartId
            var cart = _context.Carts
      .Include(c => c.CartProducts)
          .ThenInclude(cp => cp.Product)
      .FirstOrDefault(c => c.UserId == userId.Value);


            if (cart == null)
            {
                // If no cart exists, create a new cart
                cart = new Cart()
                {
                    UserId = 1,
                };
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

        public IActionResult Index()
        {
            // Retrieve the logged-in user’s ID from session
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                // No user in session, maybe redirect to login or show empty cart
                return RedirectToAction("Login", "Account");
            }

            // Lookup the cart by this user
            var cart = _context.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefault(c => c.UserId == userId.Value);

            if (cart == null || cart.CartProducts == null || !cart.CartProducts.Any())
            {
                return View("EmptyCart");
            }

            return View(cart);







        }

    }
}