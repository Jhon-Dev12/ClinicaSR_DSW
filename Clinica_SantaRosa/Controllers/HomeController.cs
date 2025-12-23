using Microsoft.AspNetCore.Mvc;
using Clinica_SantaRosa.PL.WebApp.Models;

namespace ClinicaSR.PL.WebApp.Controllers
{
    [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
    public class HomeController : Controller
    {
        public IActionResult PanelAdministrador()
        {
            var modelo = new UsuarioViewModel
            {
                Nombres = HttpContext.Session.GetString("NombreCompleto"),
                Rol = HttpContext.Session.GetString("UsuarioRol"),
                ImgPerfil = HttpContext.Session.GetString("FotoPerfil")
            };
            return View(modelo);
        }
        public IActionResult PanelRecepcionista()
        {
            var modelo = new UsuarioViewModel
            {
                Nombres = HttpContext.Session.GetString("NombreCompleto"),
                Rol = HttpContext.Session.GetString("UsuarioRol"),
                ImgPerfil = HttpContext.Session.GetString("FotoPerfil")
            };
            return View(modelo);
        }
        public IActionResult PanelCajero()
        {
            var modelo = new UsuarioViewModel
            {
                Nombres = HttpContext.Session.GetString("NombreCompleto"),
                Rol = HttpContext.Session.GetString("UsuarioRol"),
                ImgPerfil = HttpContext.Session.GetString("FotoPerfil")
            };
            return View(modelo);
        }
    }
}
