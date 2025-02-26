using Microsoft.AspNetCore.Mvc;
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
            Servicio servicio = await this._repository.FindServicio(id);
            return View(servicio);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Servicio servicio)
        {
            await this._repository.ActualizarServicioAsync(servicio);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await this._repository.EliminarServicioAsync(id);
            return RedirectToAction("Index");
        }
    }
}
