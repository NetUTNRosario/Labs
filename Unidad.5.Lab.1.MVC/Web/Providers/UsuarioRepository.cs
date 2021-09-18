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
            string salt = hasher.GenerateSalt();
            _usuarios = new List<Usuario>()
            {
                new()
                {
                    Nombre = "Hector Aguirre",
                    Mail = "admin@example.com",
                    Clave = hasher.GenerateHash("Admin_1", salt),
                    Salt = salt, 
                    Role = UsuarioRol.Admin
                }
            };
            _hasher = hasher;
        }
        public UsuarioLogeado? Validar(LoginViewModel loginVM)
        {
            var user = _usuarios.Find(u => u.Mail == loginVM.Mail && _hasher.GenerateHash(loginVM.Password, u.Salt) == u.Clave);

            return user == null? null : new UsuarioLogeado(id: user.Id, nombre: user.Nombre, mail: user.Mail, user.Role);
        }

        public UsuarioLogeado? Register(RegisterViewModel registerVM)
        {
            if (_usuarios.Find(u => u.Mail == registerVM.Mail) != null) return null;

            var salt = _hasher.GenerateSalt();
            var hashedPassword = _hasher.GenerateHash(registerVM.Clave, salt);

            Usuario user = new()
            {
                Id = _usuarios.Count() + 2,
                Nombre = registerVM.Nombre,
                Mail = registerVM.Mail,
                Clave = hashedPassword,
                Salt = salt
            };
            _usuarios.Add(user);

            return new UsuarioLogeado(id: user.Id, nombre: user.Nombre, mail: user.Mail, role: user.Role);
        }
    }
}
