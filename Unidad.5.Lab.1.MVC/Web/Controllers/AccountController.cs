using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Providers;

namespace Unidad._5.Lab._1.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioManager _usuarioManager;

        public AccountController(IUsuarioRepository usuarioRepository, IUsuarioManager usuarioManager)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioManager = usuarioManager;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            throw new NotImplementedException();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            throw new NotImplementedException();
        }
    }
}
