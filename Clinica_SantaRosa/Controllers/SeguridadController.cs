using Clinica_SantaRosa.PL.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace Clinica_SantaRosa.PL.WebApp.Controllers
{
    public class SeguridadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            UsuarioModel model = new UsuarioModel();
            return View(model);
        }

       




    }
}
