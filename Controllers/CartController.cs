using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kursinis.Data;
using Kursinis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;


namespace Kursinis.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoppingCartItems.ToListAsync());
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cartItem = await _context.ShoppingCartItems
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (cartItem == null)
            {
                return NotFound();
            }

            return View(cartItem);
        }

        

        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";

        public void AddToCart(int id)
        {
            // Retrieve the product from the database.           
            ShoppingCartId = GetCartId(HttpContext);

            var cartItem = _context.ShoppingCartItems.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.Id == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.                 
                cartItem = new CartItem
                {
                    ItemId = Guid.NewGuid().ToString(),
                    Id = id,
                    CartId = ShoppingCartId,
                    Product = _context.Products.SingleOrDefault(
                   p => p.Id == id),
                    Quantity = 1,
                    
                };

                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,                  
                // then add one to the quantity.                 
                cartItem.Quantity++;
            }
            _context.SaveChanges();
        }

        public string GetCartId(HttpContext _context)
        {
            if (_context.Session.GetString(CartSessionKey) == null)
            {
                if (!string.IsNullOrWhiteSpace(_context.User.Identity.Name))
                {
                    _context.Session.SetString(CartSessionKey, _context.User.Identity.Name);
                }
                else
                {
                    var tempCartId = Guid.NewGuid();
                    _context.Session.SetString(CartSessionKey, tempCartId.ToString());
                }
            }

            return _context.Session.GetString(CartSessionKey);
        }

        public List<CartItem> GetCartItems()
        {
            ShoppingCartId = GetCartId(HttpContext);

            return _context.ShoppingCartItems.Where(
                c => c.CartId == ShoppingCartId).ToList();
        }
    }
    
}
