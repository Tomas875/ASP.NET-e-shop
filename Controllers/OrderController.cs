using Kursinis.Data;

using Kursinis.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        
        private readonly IProducts _productService;
        private readonly IOrder _orderService;
        private readonly ShoppingCart _shoppingCart;
        public readonly UserManager<AppUser> _userManager;




        public OrderController(IOrder orderService, IProducts productService, ShoppingCart shoppingCart, UserManager<AppUser> userManager)
        {
            _orderService = orderService;
            _shoppingCart = shoppingCart;
            _productService = productService;
            _userManager = userManager;

        }

        public IActionResult Checkout()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            if (items.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some items first");
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public Order OrderIndexModelToOrder(OrderIndexModel model, AppUser user)
        {
            return new Order
            {
                Id = model.Id,
                OrderPlaced = model.OrderPlaced,
                OrderTotal = model.OrderTotal,
                Address = model.Address,
                City = model.City,
                Country = model.Country,
                ZipCode = model.ZipCode,
                //OrderLines = OrderDetailsListingModelToOrderDetails(model.OrderLines)
            };
        }


        
        [HttpPost]
        public async Task<IActionResult> Checkout(OrderIndexModel model)
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            if (items.Count() == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some items first");
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);

                model.OrderTotal = items.Sum(item => item.Amount * item.Products.Price);
                var order = OrderIndexModelToOrder(model, user);

                _orderService.CreateOrder(order);
                _shoppingCart.ClearCart();
                return RedirectToAction("CheckoutComplete");
            }

            return View(model);
        }


        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = "Thanks for your order";
            return View();
        }
    }
}