using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
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

        public IActionResult Index() => RedirectToAction("List");

        public IActionResult List()
        {
            return View(_materiaRepository.GetAll());
        }

        [Authorize(Roles = "Admin, Superadmin")]
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            Materia? materia = _materiaRepository.GetOne((int)id);
            if (materia == null) return NotFound();
            return View(new EditMateriaViewModel(materia, _planRepository.GetAll()));
        }



        [HttpPost]
        [Authorize(Roles = "Admin, Superadmin")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")]Materia materia)
        {
            if (id != materia.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _materiaRepository.Update(materia);

                return RedirectToAction("List");
            }

            return View(new EditMateriaViewModel(materia, _planRepository.GetAll()));
        }

        [Authorize]
        public IActionResult Create()
        {
            return View(new CreateMateriaViewModel(null, _planRepository.GetAll()));
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Descripcion, HsSemanales, HsTotales, PlanId")] Materia materia)
        {
            if (ModelState.IsValid)
            {
                _materiaRepository.Add(materia);

                return RedirectToAction("List");
            }

            return View(new CreateMateriaViewModel(materia, _planRepository.GetAll()));
        }

        [HttpGet]
        [Authorize(Roles = "Superadmin")]
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var materia = _materiaRepository.GetOne((int)id);

            if (materia == null) return NotFound();

            return View(materia);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Superadmin")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var materia = _materiaRepository.Delete(id);

            if (materia == null) return NotFound();

            return RedirectToAction("List");
        }
    }
}