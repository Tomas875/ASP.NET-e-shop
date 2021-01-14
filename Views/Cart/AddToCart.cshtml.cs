/*using System.Collections.Generic;
using Kursinis.Models;
using Kursinis.Data;
using Kursinis.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;

namespace Kursinis.Views.Cart
{
    public partial class AddToCart : PageModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string rawId = Request.Query["Id"];
            int productId;
            if (!String.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId))
            {
                using (CartController usersShoppingCart = new CartController())
                {
                    usersShoppingCart.AddToCart(Convert.ToInt16(rawId));
                }

            }
            else
            {
                Debug.Fail("ERROR : We should never get to AddToCart.cshtml without a ProductId.");
                throw new Exception("ERROR : It is illegal to load AddToCart.cshtml without setting a ProductId.");
            }
            Response.Redirect("ShoppingCart.cshtml");
        }
    }
}*/