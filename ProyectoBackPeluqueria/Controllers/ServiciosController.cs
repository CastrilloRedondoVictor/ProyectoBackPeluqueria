using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoBackPeluqueria.Models;
using ProyectoBackPeluqueria.Repositories;

namespace ProyectoBackPeluqueria.Controllers
{
    public class ServiciosController : Controller
    {
        RepositoryPeluqueria _repository;

        public ServiciosController(RepositoryPeluqueria repository)
        {
            this._repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            List<Servicio> servicios = await this._repository.GetServiciosAsync();
            return View(servicios);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Servicio servicio)
        {
            await this._repository.InsertarServicioAsync(servicio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            Servicio servicio = await this._repository.FindServicioAsync(id);
            return View(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Asegurar que el precio se interpreta correctamente
                    servicio.Precio = Convert.ToDecimal(servicio.Precio, System.Globalization.CultureInfo.InvariantCulture);

                    await this._repository.ActualizarServicioAsync(servicio);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar el servicio.");
                }
            }
            return View(servicio);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this._repository.EliminarServicioAsync(id);
            return RedirectToAction("Index");
        }
    }
}
