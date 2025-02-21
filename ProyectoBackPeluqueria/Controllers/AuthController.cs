using Microsoft.AspNetCore.Mvc;

namespace ProyectoBackPeluqueria.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string email)
        {
            if (username.ToLower() == "iris@gmail.com" && email == "123")
            {
                HttpContext.Session.SetInt32("idRol", 2);
                return RedirectToAction("Index", "Home");
            } else if(username.ToLower() == "victor@gmail.com" && email == "123")
            {
                HttpContext.Session.SetInt32("idRol", 1);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Error"] = "Usuario o contraseña incorrectos";
                return View();
            }
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
