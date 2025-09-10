using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuperMarketApp.Models;
using SuperMarketApp.Data;

namespace SuperMarketApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.TotalProducts = _context.Products.Count();
        ViewBag.LowStock = _context.Products.Count(p => p.Stock < 10);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
