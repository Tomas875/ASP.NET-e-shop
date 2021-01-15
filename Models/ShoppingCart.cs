using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kursinis.Data;

namespace Kursinis.Models
{
    public class ShoppingCart
    {
		private readonly ApplicationDbContext _context;

		public ShoppingCart(ApplicationDbContext context)
		{
			_context = context;
		}

		public string Id { get; set; }
		public IEnumerable<CartItem> ShoppingCartItems { get; set; }

		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
			var context = services.GetService<ApplicationDbContext>();
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

			session.SetString("CartId", cartId);
			return new ShoppingCart(context) { Id = cartId };
		}

		public bool AddToCart(Products products, int amount)
		{
			
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Products.Id == products.Id && s.ShoppingCartId == Id);
			var isValidAmount = true;
			if (shoppingCartItem == null)
			{
				if (amount >= 0)
				{
					isValidAmount = true;
				}
				shoppingCartItem = new CartItem
				{
					ShoppingCartId = Id,
					Products = products,
					Amount = (amount)
				};
				
				_context.ShoppingCartItems.Add(shoppingCartItem);
			}
			else
			{
				if (shoppingCartItem.Amount - amount >= 0)
				{
					shoppingCartItem.Amount += amount;
				}
				else
				{
					shoppingCartItem.Amount += (shoppingCartItem.Amount);
					isValidAmount = true;
				}
			}


			_context.SaveChanges();
			return isValidAmount;
		}

		public int RemoveFromCart(Products products)
		{
			var shoppingCartItem = _context.ShoppingCartItems.SingleOrDefault(
				s => s.Products.Id == products.Id && s.ShoppingCartId == Id);
			int localAmount = 0;
			if (shoppingCartItem != null)
			{
				if (shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}
				else
				{
					_context.ShoppingCartItems.Remove(shoppingCartItem);
				}
			}

			_context.SaveChanges();
			return localAmount;
		}

		public IEnumerable<CartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??
				   (ShoppingCartItems = _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
					   .Include(s => s.Products));
		}

		public void ClearCart()
		{
			var cartItems = _context
				.ShoppingCartItems
				.Where(cart => cart.ShoppingCartId == Id);

			_context.ShoppingCartItems.RemoveRange(cartItems);
			_context.SaveChanges();
		}

		public decimal GetShoppingCartTotal()
		{
			return _context.ShoppingCartItems.Where(c => c.ShoppingCartId == Id)
				.Select(c => c.Products.Price * c.Amount).Sum();
		}
	}
}
