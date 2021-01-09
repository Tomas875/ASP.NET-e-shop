using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class CartItem
    {
        public string ItemId { get; set; }

        public string CartId { get; set; }

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Products Product { get; set; }

    }
}
