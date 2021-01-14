using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Kursinis.Models;
using Kursinis.Data;

namespace Kursinis.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProducts _productService;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IProducts productService, ShoppingCart shoppingCart)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index(bool isValidAmount = true, string returnUrl = "/")
        {
            _shoppingCart.GetShoppingCartItems();

            var model = new ShoppingCartIndexModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
                ReturnUrl = returnUrl
            };

            if (!isValidAmount)
            {
                ViewBag.InvalidAmountText = "*There were not enough items in stock to add*";
            }

            return View("Index", model);
        }

        [HttpGet]
        [Route("/ShoppingCart/Add/{id}/{returnUrl?}")]
        public IActionResult Add(int id, int? amount = 1, string returnUrl = null)
        {
            var products = _productService.GetById(id);
            returnUrl = returnUrl.Replace("%2F", "/");
            bool isValidAmount = false;
            if (products != null)
            {
                isValidAmount = _shoppingCart.AddToCart(products, amount.Value);
            }

            return Index(isValidAmount, returnUrl);
        }

        public IActionResult Remove(int productId)
        {
            var products = _productService.GetById(productId);
            if (products != null)
            {
                _shoppingCart.RemoveFromCart(products);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Back(string returnUrl = "/")
        {
            return Redirect(returnUrl);
        }
    }
}

