using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class CreateMateriaViewModel
    {
        public Materia? Materia { get; }
        public List<SelectListItem> Planes { get; }
        public CreateMateriaViewModel(Materia? materia, IEnumerable<Plan> planes)
        {
            Materia = materia;
            Planes = planes
                .Select(p => new SelectListItem
                { Text = $"{p.Especialidad}:{p.Anio}", Value = p.Id.ToString() }
                ).ToList();
        }
    }
}
