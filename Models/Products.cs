using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class Products
    {
        public int Id { get; set; }
        public int Price { get; set; }

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

    }
}
