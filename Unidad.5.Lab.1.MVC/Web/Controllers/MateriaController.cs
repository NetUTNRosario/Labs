using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using System;

namespace Web.Controllers
{
    public class MateriaController : Controller
    {
        private readonly ILogger<MateriaController> _logger;

        public MateriaController(ILogger<MateriaController> logger, IMateriaRepository materiaRepository, IPlanRepository planRepository)
        {
            _logger = logger;
            _logger.LogDebug("Inicializado controlador MateriaController");
        }

        public IActionResult Index() => RedirectToAction("List");

        public IActionResult List()
        {
            throw new NotImplementedException();
        }

        [HttpGet] // Es opcional declarar esta anotacion
        public IActionResult Edit(int? id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]Materia materia)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Materia materia)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            throw new NotImplementedException();
        }
    }
}