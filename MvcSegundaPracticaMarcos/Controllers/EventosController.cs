using Microsoft.AspNetCore.Mvc;
using MvcSegundaPracticaMarcos.Services;

namespace MvcSegundaPracticaMarcos.Controllers
{
    public class EventosController : Controller
    {
        private ServiceEventos service;

        public EventosController(ServiceEventos service)
        {
            this.service = service;
        }

        // /Eventos
        public async Task<IActionResult> Index()
        {
            var eventos = await this.service.GetEventosAsync();
            return View(eventos);
        }

        // /Eventos/Categorias
        public async Task<IActionResult> Categorias()
        {
            var categorias = await this.service.GetCategoriasAsync();
            return View(categorias);
        }

        // /Eventos/PorCategoria/2
        public async Task<IActionResult> PorCategoria(int idcategoria)
        {
            var eventos = await this.service.GetEventosCategoriaAsync(idcategoria);
            ViewBag.IdCategoria = idcategoria;
            return View(eventos);
        }
    }
}