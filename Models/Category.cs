using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public partial class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
