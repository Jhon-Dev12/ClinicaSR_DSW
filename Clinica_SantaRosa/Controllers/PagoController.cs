using ClinicaSR.BL.BC;
using ClinicaSR.BL.BE;
using ClinicaSR.PL.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace ClinicaSR.PL.WebApp.Controllers
{
    public class PagoController : Controller
    {
        private readonly ComprobantePagoBC _comprobanteBC;

        public PagoController()
        {
            _comprobanteBC = new ComprobantePagoBC(); // Sin parámetros
        }
        public IActionResult Index()
        {
            var lista = _comprobanteBC.ListarComprobantes();
            return View(lista);
        }

        public IActionResult Crear()
        {
            var model = new ComprobantePagoViewModel
            {
                CitasPendientes = _comprobanteBC.ObtenerCitasPendientes() ?? new List<CitaBE>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(ComprobantePagoViewModel model)
        {
            model.CitasPendientes = _comprobanteBC.ObtenerCitasPendientes() ?? new List<CitaBE>();

            if (!ModelState.IsValid)
                return View(model);

            var citaSeleccionada = model.CitasPendientes.FirstOrDefault(c => c.ID_Cita == model.ID_Cita);
            if (citaSeleccionada == null)
            {
                ModelState.AddModelError("ID_Cita", "Debe seleccionar una cita válida.");
                return View(model);
            }

            // OBTENER ID DEL USUARIO LOGUEADO
            var idUsuario = Convert.ToInt64(HttpContext.Session.GetString("UsuarioID"));

            var obj = new ComprobantePagoBE
            {
                CitaBE = new CitaBE { ID_Cita = model.ID_Cita },
                Id_Usuario = idUsuario, // <-- asignación automática
                Nombre_Pagador = model.Nombre_Pagador,
                Apellidos_Pagador = model.Apellidos_Pagador,
                DNI_Pagador = model.DNI_Pagador,
                Contacto_Pagador = model.Contacto_Pagador,
                Monto = model.Monto,
                Metodo_Pago = model.Metodo_Pago,
                Estado = EstadoComprobante.EMITIDO,
                Fecha_Emision = DateTime.Now
            };

            _comprobanteBC.RegistrarComprobante(obj);
            TempData["Success"] = "Comprobante registrado correctamente";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Anular(long id)
        {
            try
            {
                _comprobanteBC.AnularComprobante(id);
                TempData["Success"] = "Comprobante anulado correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}