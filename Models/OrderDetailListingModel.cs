﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kursinis.Models
{
    public class OrderDetailListingModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Price { get; set; }

        public ProductSummaryModel Products { get; set; }

    }
}
