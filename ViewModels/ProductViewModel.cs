using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Kursinis.ViewModels
{
    public class ProductViewModel
    {
        public int Price { get; set; }

        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        [Display(Name = "Product Picture")]
        public IFormFile Picture { get; set; }
    }
}
