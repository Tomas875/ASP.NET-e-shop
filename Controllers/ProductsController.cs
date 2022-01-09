using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kursinis.Data;
using Kursinis.Models;
using System.Web;
using System.Net;
using System;
using Microsoft.AspNetCore.Hosting;
using Kursinis.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace Kursinis.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        { 
           return View(await _context.Products.ToListAsync());
        }
        public PartialViewResult ProductListPartial(int? category)
        {

            if (category != null)
            {
                ViewBag.category = category;
                var productList = _context.Products
                                .OrderByDescending(x => x.Id)
                                .Where(x => x.Id == category);

                return PartialView(productList);
            }
            else
            {
                var productList = _context.Products.OrderByDescending(x => x.Id);
                return PartialView(productList);
            }
        }


        // GET: ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }
        // Post: ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.Products.Where( j => j.ItemName.Contains(SearchPhrase)).ToListAsync());
        }
        public async Task<IActionResult> SearchByName(string ProdSearch)
        {
            ViewData["GetProducts"] = ProdSearch;
            return View("Index", await _context.Products.Where(j => j.ItemName.Contains(ProdSearch)).ToListAsync());
        }
        


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
        //[Authorize(Roles = "Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Products/Create
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemName,ItemDescription,Price")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(products);
        }

        /*private string UploadedFile(ProductViewModel model)
        {
            string uniqueFileName = null;

            if (model.Picture != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Picture.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }*/
        //[Authorize(Roles = "Admin, Mod")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            
            return View(products);
        }
        //[Authorize(Roles = "Admin,Mod")]
        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemName,ItemDescription,Price")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            
            return View(products);
        }
        //[Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        //[Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            _context.Products.Remove(products);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        

    }
}
