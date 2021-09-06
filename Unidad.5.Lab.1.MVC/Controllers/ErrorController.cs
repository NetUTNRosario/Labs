using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unidad._5.Lab._1.MVC.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/error/404")]
        public IActionResult NotFoundError()
        {
            return View();
        }

        [Route("/error/{code:int}")]
        public IActionResult GenericError(int code)
        {
            _logger.LogInformation($"Error codigo {code}");
            return View();
        }
    }
}