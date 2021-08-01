using System;

namespace Modelo
{
    public class Plan
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int IdEspecialidad { get; set; }
        public Especialidad Especialidad { get; set; }
    }
}