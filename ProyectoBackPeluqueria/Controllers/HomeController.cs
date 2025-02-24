using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoBackPeluqueria.Models;

namespace ProyectoBackPeluqueria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("idRol") == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            if (HttpContext.Session.GetInt32("idRol") == null)
            {
                return RedirectToAction("Denied", "Auth");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
