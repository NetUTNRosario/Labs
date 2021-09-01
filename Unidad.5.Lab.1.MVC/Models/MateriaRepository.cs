namespace Unidad._5.Lab._1.MVC.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IMateriaRepository" />.
    /// </summary>
    public interface IMateriaRepository
    {
        /// <summary>
        /// The GetMaterias.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Materia}"/>.</returns>
        IEnumerable<Materia> GetMaterias();

        /// <summary>
        /// The GetOneMateria.
        /// </summary>
        /// <param name="materiaId">The materiaId<see cref="int"/>.</param>
        /// <returns>The <see cref="Materia?"/>.</returns>
        Materia? GetOneMateria(int materiaId);

        /// <summary>
        /// The UpdateMateria.
        /// </summary>
        /// <param name="materia">The materia<see cref="Materia"/>.</param>
        void UpdateMateria(Materia materia);
    }

    /// <summary>
    /// Defines the <see cref="MateriaRepository" />.
    /// </summary>
    public class MateriaRepository : IMateriaRepository
    {
        /// <summary>
        /// Defines the _materias.
        /// </summary>
        private List<Materia> _materias;

        /// <summary>
        /// Initializes a new instance of the <see cref="MateriaRepository"/> class.
        /// </summary>
        public MateriaRepository(IPlanRepository planRepository)
        {
            _materias = new List<Materia>() {
                new()
                {
                    Id=1,
                    Descripcion = "Sistemas de Gestión",
                    HsSemanales = 4,
                    HsTotales = 136,
                    PlanId = 1,
                    Plan = planRepository.GetOnePlan(1)!
                },
                new()
                {
                    Id=2,
                    Descripcion = "Quimica Organica",
                    HsSemanales = 6,
                    HsTotales = 102,
                    PlanId = 2,
                    Plan = planRepository.GetOnePlan(2)!
                }
            };
        }

        /// <summary>
        /// The GetMaterias.
        /// </summary>
        /// <returns>The <see cref="IEnumerable{Materia}"/>.</returns>
        public IEnumerable<Materia> GetMaterias() => _materias;

        /// <summary>
        /// The GetOneMateria.
        /// </summary>
        /// <param name="materiaId">The materiaId<see cref="int"/>.</param>
        /// <returns>The <see cref="Materia?"/>.</returns>
        public Materia? GetOneMateria(int materiaId) => _materias.Find(m => m.Id == materiaId);

        /// <summary>
        /// The UpdateMateria.
        /// </summary>
        /// <param name="materia">The materia<see cref="Materia"/>.</param>
        public void UpdateMateria(Materia materia)
        {
            _materias[_materias.FindIndex(m => m.Id == materia.Id)] = materia;
        }
    }
}
