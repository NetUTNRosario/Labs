using System;

namespace Model
{
    public class Plan
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public int EspecialidadId { get; set; }
        public Especialidad Especialidad { get; set; }
    }
}