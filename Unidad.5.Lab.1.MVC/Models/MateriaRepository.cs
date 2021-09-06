namespace Unidad._5.Lab._1.MVC.Models
{
    using System;
    using System.Collections.Generic;

    public interface IMateriaRepository
    {
        IEnumerable<Materia> GetAlll();

        Materia? GetOne(int materiaId);

        void Update(Materia materia);

        void Add(Materia materia);
    }

    public class MateriaRepository : IMateriaRepository
    {
        private List<Materia> _materias;

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
                    Plan = planRepository.GetOne(1)!
                },
                new()
                {
                    Id=2,
                    Descripcion = "Quimica Organica",
                    HsSemanales = 6,
                    HsTotales = 102,
                    PlanId = 2,
                    Plan = planRepository.GetOne(2)!
                }
            };
        }

        public IEnumerable<Materia> GetAlll() => _materias;

        public Materia? GetOne(int materiaId) => _materias.Find(m => m.Id == materiaId);

        public void Update(Materia materia)
        {
            int? materiaIndex = _materias.FindIndex(m => m.Id == materia.Id);
            if (materiaIndex == null)
            {
                throw new Exception("Materia not found in the current data");
            }
            _materias[materiaIndex.Value] = materia;
        }

        public void Add(Materia materia)
        {
            materia.Id = _materias.Count + 2;
            _materias.Add(materia);
        }
    }
}
