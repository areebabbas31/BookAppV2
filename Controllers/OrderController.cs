using System;
using System.Linq;
using System.Threading.Tasks;
using BulkyBookWeb.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Action to place an order (checkout)
        [HttpPost]
        public IActionResult Checkout()
        {
            // You might retrieve the user id from session or claims
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                // Handle not logged in user (redirect, error, etc.)
                return RedirectToAction("Index", "Home");
            }

            // Fetch the cart with products
            var cart = _context.Carts
                .Include(c => c.CartProducts)
                    .ThenInclude(cp => cp.Product)
                .FirstOrDefault(c => c.UserId == userId.Value);

            if (cart == null || !cart.CartProducts.Any())
            {
                // No cart or empty cart
                return RedirectToAction("EmptyCart", "Cart");
            }

            // Create Order
            var order = new Order
            {
                UserId = userId.Value,
                OrderDate = DateTime.Now,
                TotalAmount = cart.CartProducts.Sum(cp => cp.Quantity * cp.Product.Price),
            };

            _context.Orders.Add(order);
   _context.SaveChanges();

            // Create order products
            foreach (var cartProduct in cart.CartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = cartProduct.ProductId,
                    Quantity = cartProduct.Quantity,
                    PriceAtTimeOfOrder = cartProduct.Product.Price
                };
                _context.OrderProducts.Add(orderProduct);
            }

            _context.SaveChanges();

            // Clear cart
            _context.CartProducts.RemoveRange(cart.CartProducts);
            _context.Carts.Remove(cart);
            _context.SaveChanges();

            // Redirect to order success page, passing orderId
            return RedirectToAction("OrderSuccess", new { orderId = order.OrderId });
        }

        // Display order success (details)
        public IActionResult OrderSuccess(int orderId)
        {
            

            return View();
        }
    }

}
