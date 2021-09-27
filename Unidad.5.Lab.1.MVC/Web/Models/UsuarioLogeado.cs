using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class UsuarioLogeado {
        public int Id { get; }
        public string Nombre { get; }
        public string Mail { get; }
        public UsuarioRol Role { get; set; }
        public DateTime MomentoCreacion { get; }

        public UsuarioLogeado(int id, string nombre, string mail, UsuarioRol role, DateTime? momentoCreacion = null)
        {
            Id = id;
            Nombre = nombre;
            Mail = mail;
            Role = role;
            MomentoCreacion = momentoCreacion ?? DateTime.Now;
        }
    }
}
