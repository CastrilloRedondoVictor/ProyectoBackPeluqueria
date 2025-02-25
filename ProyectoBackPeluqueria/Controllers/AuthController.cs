using Microsoft.AspNetCore.Mvc;
using ProyectoBackPeluqueria.Models;
using ProyectoBackPeluqueria.Repositories;

namespace ProyectoBackPeluqueria.Controllers
{
    public class AuthController : Controller
    {
        public RepositoryPeluqueria _repository;

        public AuthController(RepositoryPeluqueria repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario = await _repository.LoginAsync(email, password);
            if (usuario != null)
            {
                HttpContext.Session.SetInt32("idRol", usuario.IdRolUsuario);
                HttpContext.Session.SetInt32("idUsuario", usuario.Id);
                HttpContext.Session.SetString("NombreUsuario", usuario.Nombre);
                return RedirectToAction("Index", "Home");
            } else
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
