using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web.Providers;

namespace Web.Models
{
    public interface IUsuarioRepository
    {
        UsuarioLogeado? Validar(LoginViewModel loginVM);
        UsuarioLogeado? Register(RegisterViewModel registerVM);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private List<Usuario> _usuarios;
        private readonly IHasher _hasher;

        public UsuarioRepository(IHasher hasher)
        {
            _usuarios = new List<Usuario>();
            _hasher = hasher;

            string saltHector = hasher.GenerateSalt();
            _usuarios.Add(new()
            {
                Nombre = "Hector Aguirre",
                Mail = "admin@example.com",
                Clave = hasher.GenerateHash("Admin_1", saltHector),
                Salt = saltHector,
                Role = UsuarioRol.Admin
            });
            string saltMario = hasher.GenerateSalt();
            _usuarios.Add(new()
            {
                Nombre = "Mario Eche",
                Mail = "superadmin@example.com",
                Clave = hasher.GenerateHash("Superadmin_1", saltMario),
                Salt = saltMario,
                Role = UsuarioRol.Superadmin
            });
        }
        public UsuarioLogeado? Validar(LoginViewModel loginVM)
        {
            var user = _usuarios.Find(u => "completar");

            return user == null? null : new UsuarioLogeado(id: user.Id, nombre: user.Nombre, mail: user.Mail, user.Role);
        }

        public UsuarioLogeado? Register(RegisterViewModel registerVM)
        {
            if (_usuarios.Find(u => u.Mail == registerVM.Mail) != null) return null;

            Usuario user = new();
            _usuarios.Add(user);

            return new UsuarioLogeado(id: user.Id, nombre: user.Nombre, mail: user.Mail, role: user.Role);
        }
    }
}
