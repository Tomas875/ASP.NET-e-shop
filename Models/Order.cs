using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class Order
    {
        public int Id { get; set; }

        public IEnumerable<OrderDetail> OrderLines { get; set; }

        public string ZipCode { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }
        






    }
}
