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

    public class MateriaValidator: AbstractValidator<Materia>
    {
        public MateriaValidator()
        {
            RuleFor(m => m.Descripcion).NotEmpty().Length(min: 3, max: 20);
            RuleFor(m => m.HsSemanales).NotEmpty().InclusiveBetween(from: 2, to: 6);
            RuleFor(m => m.HsTotales).NotEmpty().InclusiveBetween(from: 90, to: 150);
            RuleFor(m => m.PlanId).NotEmpty();
        }
    }
}