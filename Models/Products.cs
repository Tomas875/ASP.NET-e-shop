using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public partial class Products
    {
        public int Id { get; set; }
        public int Price { get; set; }

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        
        public int CategoryId { get; set; }
        
        public virtual  Category Category { get; set; }
        



    }
}
