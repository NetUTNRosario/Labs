using System.Collections.Generic;

namespace Unidad._5.Lab._1.MVC.Models
{
    public interface IMateriaRepository
    {
        IEnumerable<Materia> GetMaterias();
        Materia GetOneMateria(int materiaId);
        void AddMateria(Materia materia);
    }

    public class MateriaRepository : IMateriaRepository
    {
        private List<Materia> _materias;

        public MateriaRepository()
        {
            _materias = new List<Materia>() {
                                new()
                {
                    Descripcion = "Sistemas de Gestión",
                    HsSemanales = 4,
                    HsTotales = 136,
                    Plan = new Plan()
                    {
                        Anio = 2008,
                        Especialidad = new Especialidad()
                        {
                            Descripcion = "Ingeniería en Sistemas de Información"
                        }
                    },
                },
                new()
                {
                    Descripcion = "Administración Gerencial",
                    HsSemanales = 6,
                    HsTotales = 102,
                    Plan = new Plan()
                    {
                        Anio = 2008,
                        Especialidad = new()
                        {
                            Descripcion = "Ingeniería en Sistemas de Información"
                        }
                    },
                }
            };
        }

        public IEnumerable<Materia> GetMaterias() => _materias;
        public Materia GetOneMateria(int materiaId) => _materias.Find(m => m.Id == materiaId);
        public void AddMateria(Materia materia)
        {
            _materias.Add(materia);
        }
    }
}