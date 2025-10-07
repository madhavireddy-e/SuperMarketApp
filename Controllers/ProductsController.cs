using Microsoft.AspNetCore.Mvc;
using SuperMarketApp.Models;
using System.Linq;
using SuperMarketApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace SuperMarketApp.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Products
        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET: /Products/Details/5
        [HttpGet("Details/{id}")]
        [Authorize]
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
                return NotFound(); // 404 if not found

            return View(product);
        }

        // GET: /Products/Create
        [Authorize(Roles = "Admin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            if (CurrentUser.Role != UserRole.Admin)
                return Unauthorized(); // 401 if not admin

            return View();
        }

        // POST: /Products/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Product product)
        {
            if (CurrentUser.Role != UserRole.Admin)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: /Products/Edit/5
        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Products/Edit/5
        [HttpPost("Edit")]
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
        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: /Products/Delete/5
        [HttpPost("Delete")]
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
