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

        public MateriaController(ILogger<MateriaController> logger, IMateriaRepository materiaRepository)
        {
            _logger = logger;
            _materiaRepository = materiaRepository;
            _logger.LogDebug("Inicializado controlador MateriaController");
        }

        public IActionResult List()
        {
            return View(_materiaRepository.GetMaterias());
        }

        public IActionResult Edit(int materiaId)
        {
            return View(_materiaRepository.GetOneMateria(materiaId));
        }
    }
}