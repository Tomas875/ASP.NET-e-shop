using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class CartItem
    {
        
        public string Id { get; set; }
        public virtual Products Products { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }

    }
}
