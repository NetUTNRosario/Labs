using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unidad._5.Lab._1.MVC.Models;

namespace Unidad._5.Lab._1.MVC.Controllers
{
    public class MateriaController : Controller
    {
        private readonly ILogger<MateriaController> _logger;
        private readonly IMateriaRepository _materiaRepository;
        private readonly IPlanRepository _planRepository;

        public MateriaController(ILogger<MateriaController> logger, IMateriaRepository materiaRepository, IPlanRepository planRepository)
        {
            _logger = logger;
            _materiaRepository = materiaRepository;
            _planRepository = planRepository;
            _logger.LogDebug("Inicializado controlador MateriaController");
        }

        public IActionResult List()
        {
            return View(_materiaRepository.GetMaterias());
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            Materia? materia = _materiaRepository.GetOneMateria((int)id);
            if (materia == null) return NotFound();
            return View(new EditMateriaViewModel(materia, _planRepository.GetPlanes()));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]Materia materia)
        {
            if (id != materia.Id) return NotFound();
            if (ModelState.IsValid)
            {
                materia.Plan = _planRepository.GetOnePlan(materia.PlanId);
                _materiaRepository.UpdateMateria(materia);
            }

            return RedirectToAction("List");
        }
    }
}