using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Web.Models
{
    public class Materia
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Descripcion { get; set; }
        [Required]
        [Range(2, 6)]
        [Display(Name = "Horas Semanales")]
        public int HsSemanales { get; set; }
        [Required]
        [Range(90, 150)]
        [Display(Name = "Horas Totales")]
        public int HsTotales { get; set; }
        [Required]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }
    }
}