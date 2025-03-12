using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProyectoBackPeluqueria.Models;
using ProyectoBackPeluqueria.Repositories;
using ProyectoBackPeluqueria.Filters;
using System.Security.Claims;

namespace ProyectoBackPeluqueria.Controllers
{
    public class ManagementController : Controller
    {
        RepositoryPeluqueria _repository;

        public ManagementController(RepositoryPeluqueria repository)
        {
            _repository = repository;
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Index(int? id)
        {
            if(id == null)
            {
                id = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            Usuario usuario = await _repository.FindUsuario(id.Value);

            Reserva proximaReserva = await _repository.GetProximaReservaUsuarioAsync(id.Value);

            ViewData["ProximaReserva"] = proximaReserva;
            ViewData["ServicioProximaReserva"] = proximaReserva != null ? await _repository.GetServicioReservaAsync(proximaReserva.ServicioId) : null;

            return View(usuario);
        }
        public async Task<IActionResult> Edit(int id)

        {
            Usuario usuario = await _repository.FindUsuario(id);
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario, string adminCode)
        {
            if (!string.IsNullOrEmpty(adminCode) && adminCode == "Taj@mar365")
            {
                usuario.IdRolUsuario = 2; // Administrador
            }
            else
            {
                usuario.IdRolUsuario = 1; // Usuario normal
            }
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

            await _repository.UpdateUsuarioAsync(usuario);
            return RedirectToAction("Index");
        }




        [HttpPost]
        public async Task<IActionResult> AgregarRangoDisponibilidad(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio > fechaFin)
            {
                return BadRequest("La fecha de inicio no puede ser mayor que la fecha de fin.");
            }

            try
            {
                var resultado = await _repository.AgregarDisponibilidadRangoAsync(fechaInicio, fechaFin);

                return Ok(new
                {
                    diasAgregados = resultado.diasAgregados,
                    diasExistentes = resultado.diasExistentes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al agregar disponibilidad: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerEventosCalendario()
        {
            var eventos = new List<object>();

            // Obtener citas con fecha y hora de inicio y fin
            var citas = await _repository.ObtenerCitasConHoras();

            // Añadir cada cita al evento, incluyendo la fecha de inicio, fecha de fin y el servicio
            eventos.AddRange(citas.Select(c => new
            {
                id = c.ReservaId, // ID de la cita
                title = c.Servicio,  // El título será el nombre del servicio
                start = c.FechaInicio.ToString("yyyy-MM-ddTHH:mm:ss"), // Fecha de inicio en formato ISO
                end = c.FechaFin.ToString("yyyy-MM-ddTHH:mm:ss"), // Fecha de fin en formato ISO
            }));

            return Json(eventos);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerDiasDisponibles()
        {
            var diasDisponibles = await _repository.ObtenerDiasDisponibles();
            return Json(diasDisponibles.Select(d => new { start = d.ToString("yyyy-MM-dd") }));
        }

        [HttpGet]
        public async Task<IActionResult> GetDetallesReserva(int id)
        {
            var reserva = await _repository.FindReservaAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            return Json(reserva);
        }





    }
}
