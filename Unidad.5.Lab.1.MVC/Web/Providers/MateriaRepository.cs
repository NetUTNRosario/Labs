namespace Web.Models
{
    using System;
    using System.Collections.Generic;

    public interface IMateriaRepository
    {
        IEnumerable<Materia> GetAll();

        Materia? GetOne(int materiaId);

        void Update(Materia materia);

        void Add(Materia materia);

        Materia? Delete(int id);
    }

    public class MateriaRepository : IMateriaRepository
    {
        private List<Materia> _materias;
        private readonly IPlanRepository _planRepository;

        public MateriaRepository(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
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

        public IEnumerable<Materia> GetAll() => _materias;

        public Materia? GetOne(int materiaId) => _materias.Find(m => m.Id == materiaId);

        public void Update(Materia materia)
        {
            int? materiaIndex = _materias.FindIndex(m => m.Id == materia.Id);
            if (materiaIndex == null)
            {
                throw new Exception("Materia not found in the current data");
            }
            materia.Plan = _planRepository.GetOne(materia.PlanId);
            _materias[materiaIndex.Value] = materia;
        }

        public void Add(Materia materia)
        {
            materia.Id = _materias.Count + 2;
            materia.Plan = _planRepository.GetOne(materia.PlanId);
            _materias.Add(materia);
        }

        public Materia? Delete(int id)
        {
            var materiaToDelete = _materias.Find(m => m.Id == id);

            if (materiaToDelete == null) return null;

            _materias.Remove(materiaToDelete);

            return materiaToDelete;
        }
    }
}
