/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Kursinis.Models;
using Kursinis.Controllers;

namespace Kursinis.Views.Cart
{
    public partial class ShoppingCartModel : PageModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public List<CartItem> GetShoppingCartItems()
        {
            CartController actions = new CartController();
            return actions.GetCartItems();
        }
    }
}
*/