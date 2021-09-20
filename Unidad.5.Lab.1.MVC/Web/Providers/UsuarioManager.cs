using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Providers
{
    public interface IUsuarioManager
    {
        Task SignIn(HttpContext httpContext, UsuarioLogeado usuarioLogeado, bool isPersistent = false);
        Task SignOut(HttpContext httpContext);
    }

    public class UsuarioManager : IUsuarioManager
    {
        public async Task SignIn(HttpContext httpContext, UsuarioLogeado usuarioLogeado, bool isPersistent = false)
        {
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, usuarioLogeado.Id.ToString()),
                new(ClaimTypes.Name, usuarioLogeado.Nombre),
                new(ClaimTypes.Email, usuarioLogeado.Mail),
                new(ClaimTypes.Role, usuarioLogeado.Role.ToString())
            };

            string authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            var claimPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, authScheme));

            await httpContext.SignInAsync(authScheme, claimPrincipal, new AuthenticationProperties() { IsPersistent = isPersistent });
        }

        public async Task SignOut(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
