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
            bool isValidAmount = true;
            if (products != null)
            {
                isValidAmount = _shoppingCart.AddToCart(products, amount.Value);
            }

            return Index(isValidAmount, returnUrl);
        }
        [HttpGet]
        [Route("/ShoppingCart/Remove/{id}/{returnUrl?}")]
        public IActionResult Remove(int productsId)
        {
            var products = _productService.GetById(productsId);
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

