using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            // Verificar si el usuario existe
            Usuario usuario = await _repository.LoginAsync(email, password);

            if (usuario == null)
            {
                ViewData["Error"] = "Credenciales incorrectas";
                return View();
            }

            // Crear identidad y claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.Nombre ?? ""),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
        new Claim(ClaimTypes.Email, usuario.Email ?? ""),
        new Claim("Apellidos", usuario.Apellidos ?? ""),
        new Claim("Telefono", usuario.Telefono ?? ""),
        new Claim(ClaimTypes.Role, usuario.IdRolUsuario == 2 ? "Admin" : "User") // Definir rol
    };

            // Crear identidad y principal
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(identity);

            // Iniciar sesión con autenticación por cookies
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal,
                new AuthenticationProperties
                {
                    IsPersistent = true, // Mantiene la sesión activa
                    ExpiresUtc = DateTime.UtcNow.AddHours(2) // Expira en 2 horas
                });

            return RedirectToAction("Index", "Home"); // Redirigir después del login
        }



        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Usuario usuario)
        {
            await _repository.RegisterAsync(usuario);
            return RedirectToAction("Login");
        }

        public IActionResult Denied()
        {
            return View();
        }
    }
}
