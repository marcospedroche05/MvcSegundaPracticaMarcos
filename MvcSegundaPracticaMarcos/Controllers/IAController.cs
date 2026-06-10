using Microsoft.AspNetCore.Mvc;
using MvcSegundaPracticaMarcos.Services;

namespace MvcSegundaPracticaMarcos.Controllers
{
    public class IAController : Controller
    {
        private ServiceIA service;

        public IAController(ServiceIA service)
        {
            this.service = service;
        }

        // GET /IA
        public IActionResult Index()
        {
            return View();
        }

        // POST /IA
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string pregunta)
        {
            if (!string.IsNullOrEmpty(pregunta))
            {
                try
                {
                    string respuesta = await this.service.PreguntarAsync(pregunta);
                    ViewBag.Pregunta = pregunta;
                    ViewBag.Respuesta = respuesta;
                }
                catch (Exception ex)
                {
                    ViewBag.Pregunta = pregunta;
                    ViewBag.Respuesta = $"ERROR: {ex.Message}";
                }
            }
            return View();
        }
    }
}