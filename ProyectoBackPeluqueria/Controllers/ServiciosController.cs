using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoBackPeluqueria.Filters;
using NugetProyectoBackPeluqueria.Models;
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

        [AuthorizeUsers]
        public async Task<IActionResult> Index()
        {
            List<Servicio> servicios = await this._repository.GetServiciosAsync();
            return View(servicios);
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Servicio servicio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Verificar y corregir el precio en caso de problemas con la cultura
                    var precioTexto = Request.Form["Precio"].ToString().Replace(",", ".");
                    servicio.Precio = decimal.Parse(precioTexto, System.Globalization.CultureInfo.InvariantCulture);

                    await this._repository.InsertarServicioAsync(servicio);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al guardar el servicio.");
                }
            }
            return View();
        }

        [AuthorizeUsers]
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
                    // Verificar y corregir el precio en caso de problemas con la cultura
                    var precioTexto = Request.Form["Precio"].ToString().Replace(",", ".");
                    servicio.Precio = decimal.Parse(precioTexto, System.Globalization.CultureInfo.InvariantCulture);

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
