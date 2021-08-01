using System;
using Xunit;

namespace RepositorioTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        private void SeedTestDb(){
            var especialidades = new List<Especialidad>()
            {
                new()
                {
                    Descripcion = "Ingeniería en Sistemas de Información"
                },
                new()
                {
                    Descripcion = "Ingeniería Química"
                },
                new()
                {
                    Descripcion = "Ingeniería Eléctrica"
                },
                new()
                {
                    Descripcion = "Ingeniería Mecánica"
                },
                new()
                {
                    Descripcion = "Ingeniería Civil"
                }
            };
            
            
            var planes = new List<Plan>()
            {
                new()
                {
                    Descripcion = "2008",
                    IDEspecialidad = 1
                },
                new()
                {
                    Descripcion = "1995",
                    IDEspecialidad = 1
                },
                new()
                {
                    Descripcion = "1994",
                    IDEspecialidad = 4
                },
                new()
                {
                    Descripcion = "2009",
                    IDEspecialidad = 3
                }
            };

            var materias = new List<Materia>
            {
                new()
                {
                    Descripcion = "Sistemas de Gestión",
                    HSSemanales = 4,
                    HSTotales = 136,
                    IDPlan = 1
                },
                new()
                {
                    Descripcion = "Administración Gerencial",
                    HSSemanales = 6,
                    HSTotales = 102,
                    IDPlan = 1
                }
            };
        }
    }
}
