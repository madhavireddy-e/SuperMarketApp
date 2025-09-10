using Microsoft.AspNetCore.Mvc;
using SuperMarketApp.Models;
using System.Linq;
using SuperMarketApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace SuperMarketApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }


        [Authorize]
        public IActionResult Details(int id)
        {

            // Find product by Id
            // var product = products.FirstOrDefault(p => p.Id == id);
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound(); // If no product, return 404
            }

            return View(product);
        }

        // GET: /Products/Create
        [Authorize(Roles = "Admin")]
        
        public IActionResult Create()
        {
            if (CurrentUser.Role != UserRole.Admin)
                return Unauthorized(); // 401 page

            return View();
        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Product product)
        {
            if (CurrentUser.Role != UserRole.Admin)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);   // Insert into DB
                _context.SaveChanges();           // Commit changes
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Product product)
        {
            if (CurrentUser.Role != UserRole.Admin)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: /Products/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            if (CurrentUser.Role != UserRole.Admin)
                return Unauthorized();

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
    }
} 
