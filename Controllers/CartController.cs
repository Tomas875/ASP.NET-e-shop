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
using System.Web;
using Microsoft.AspNetCore.Session;


namespace Kursinis.Controllers
{
    [Authorize]
    public class CartController : Controller
    {

        // GET: ShoppingCart
        public async Task<IActionResult> Index()
        {
            Cart cart = Cart["ShoppingCart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["ShoppingCart"] = cart;
            }
            return View(cart);
        }

        public ActionResult CreateOrUpdate(CartViewModel value)
        {
            Cart cart = (Cart)Session["ShoppingCart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["ShoppingCart"] = cart;
            }
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Products product = context.Products.Find(value.Id);
                if (product != null)
                {
                    if (value.Quantity == 0)
                    {
                        cart.AddItem(value.Id, product);
                    }
                    else
                    {
                        cart.SetItemQuantity(value.Id, value.Quantity, product);
                    }
                }
            }

            Session["CartCount"] = cart.GetItems().Count();
            return View("Index", cart);
        }

        

        
    }
}
