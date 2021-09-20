using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public enum UsuarioRol
    {
        User = 1,
        Admin = 2,
        Superadmin = 3
    }

    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string Clave { get; set; }
        public string Salt { get; set; }
        public UsuarioRol Role { get; set; } = UsuarioRol.User;
    }
}
