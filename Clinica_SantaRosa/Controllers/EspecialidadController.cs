using Clinica_SantaRosa.PL.WebApp.Models;
using ClinicaSR.BL.BE;
using ClinicaSR.PL.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaSR.PL.WebApp.Controllers
{
    public class EspecialidadController : Controller
    {
        private EspecialidadModel model = new EspecialidadModel();

        // LISTAR
        public IActionResult Index()
        {
            var lista = model.Listar();
            return View(lista);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View(new EspecialidadViewModel());
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EspecialidadViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            EspecialidadBE be = new EspecialidadBE
            {
                Nombre = vm.Nombre
            };

            int id = model.Registrar(be);

            if (id == 0)
            {
                ModelState.AddModelError("", "La especialidad ya existe");
                return View(vm);
            }

            TempData["Success"] = "Especialidad registrada correctamente";
            return RedirectToAction(nameof(Index));
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {
            var especialidad = model.Listar()
                                    .FirstOrDefault(e => e.ID_Especialidad == id);

            if (especialidad == null)
                return RedirectToAction(nameof(Index));

            EspecialidadViewModel vm = new EspecialidadViewModel
            {
                ID_Especialidad = (int)especialidad.ID_Especialidad,
                Nombre = especialidad.Nombre
            };

            return View(vm);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EspecialidadViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            EspecialidadBE be = new EspecialidadBE
            {
                ID_Especialidad = vm.ID_Especialidad,
                Nombre = vm.Nombre
            };

            bool ok = model.Actualizar(be);

            if (!ok)
            {
                ModelState.AddModelError("", "No se pudo actualizar la especialidad");
                return View(vm);
            }

            TempData["Success"] = "Especialidad actualizada correctamente";
            return RedirectToAction(nameof(Index));
        }

        // DELETE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            bool ok = model.Eliminar(id);

            if (!ok)
                TempData["Error"] = "No se puede eliminar la especialidad (esta en uso)";
            else
                TempData["Success"] = "Especialidad eliminada correctamente";

            return RedirectToAction(nameof(Index));
        }
    }
}