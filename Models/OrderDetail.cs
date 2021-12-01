using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductsId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }
        public virtual Products Products { get; set; }
        public virtual Order Order { get; set; }





    }
}
