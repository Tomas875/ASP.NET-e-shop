using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class ShoppingCartIndexModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public decimal ShoppingCartTotal { get; set; }
        public string ReturnUrl { get; set; }
    }
}
