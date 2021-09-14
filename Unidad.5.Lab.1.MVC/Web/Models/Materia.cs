using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Models
{
    public class Materia
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        [Display(Name = "Horas Semanales")]
        public int HsSemanales { get; set; }
        [Display(Name = "Horas Totales")]
        public int HsTotales { get; set; }
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }
    }
}