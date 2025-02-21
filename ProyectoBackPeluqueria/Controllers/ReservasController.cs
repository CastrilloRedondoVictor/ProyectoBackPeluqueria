using Microsoft.AspNetCore.Mvc;
using ProyectoBackPeluqueria.Repositories;

namespace ProyectoBackPeluqueria.Controllers
{
    public class ReservasController : Controller
    {

        private RepositoryPeluqueria _repository;

        public ReservasController(RepositoryPeluqueria repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("idRol") == null)
            {
                return RedirectToAction("Denied", "Auth");
            }
            var reservas = await _repository.ObtenerReservasClientesAsync();
            return View(reservas);
        }


        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetInt32("idRol") == null)
            {
                return RedirectToAction("Denied", "Auth");
            }
            var clientes = await _repository.GetClientesAsync();
            var servicios = await _repository.ObtenerServiciosAsync();
            ViewData["Clientes"] = clientes;
            return View(servicios);
        }


        // Obtener los horarios disponibles por servicio y fecha
        [HttpGet]
        public async Task<IActionResult> ObtenerHorariosDisponibles(int servicioId, string fecha)
        {
            var fechaDate = DateTime.Parse(fecha); // Convertir el string de fecha a DateTime
            var horarios = await _repository.ObtenerHorariosDisponiblesPorFechaAsync(servicioId, fechaDate);

            var horariosDisponibles = horarios.Where(h => h.Disponible).Select(h => new
            {
                id = h.Id,
                horaInicio = h.HoraInicio.ToString(@"hh\:mm")
            }).ToList();

            return Json(horariosDisponibles);
        }

        // Insertar una nueva reserva
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> InsertarReserva(int clienteId, int servicioId, string fechaHoraInicio)
        {
            var fechaHora = DateTime.Parse(fechaHoraInicio);
            await _repository.InsertarReservaAsync(clienteId, servicioId, fechaHora);

            return Ok();
        }
    }
}
