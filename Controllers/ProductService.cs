using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kursinis.Data;
using Kursinis.Models;
using System.Text.RegularExpressions;

namespace Kursinis.Controllers
{
    public class ProductService : IProducts
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetAll()
        {
            return _context.Products;
        }
        public Products GetById(int id)
        {
            return GetAll().FirstOrDefault(products => products.Id == id);
        }

    }
}
