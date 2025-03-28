using Microsoft.AspNetCore.Mvc;
using NugetProyectoBackPeluqueria.Models;
using ProyectoBackPeluqueria.Repositories;

namespace ProyectoBackPeluqueria.Controllers
{
    public class ClientesController : Controller
    {
        RepositoryPeluqueria _repository;

        public ClientesController(RepositoryPeluqueria repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            List<Usuario> clientes = await _repository.GetClientesAsync();
            return View(clientes);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario, string adminCode)
        {
            if (!string.IsNullOrEmpty(adminCode) && adminCode == "Taj@mar365")
            {
                usuario.IdRolUsuario = 2; // Administrador
            }
            else
            {
                usuario.IdRolUsuario = 1; // Usuario normal
            }

            usuario.Contrasena = BCrypt.Net.BCrypt.HashPassword(usuario.Contrasena);

            string iniciales = AuthController.GetIniciales(usuario.Nombre + " " + usuario.Apellidos);

            // Paso 4
            byte[] imagenAvatar = AuthController.GenerarAvatar(iniciales, usuario.ColorAvatar);

            // Paso 5
            string carpetaAvatar = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/avatars");

            // Paso 6
            if (!Directory.Exists(carpetaAvatar))
            {
                Directory.CreateDirectory(carpetaAvatar);
            }

            // Paso 7
            string nombreAvatar = $"{Guid.NewGuid()}.png";

            // Paso 8
            string nombreArchivo = Path.Combine(carpetaAvatar, nombreAvatar);

            // Paso 9
            System.IO.File.WriteAllBytes(nombreArchivo, imagenAvatar);

            // Paso 10
            usuario.Imagen = nombreAvatar;

            await _repository.RegisterAsync(usuario);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Usuario usuario = await _repository.FindUsuario(id);
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            await _repository.UpdateUsuarioAsync(usuario);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this._repository.DeleteUsuarioAsync(id);
            return Ok();
        }
    }
}
