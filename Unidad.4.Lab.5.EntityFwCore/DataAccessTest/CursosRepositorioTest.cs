using System;
using System.Collections.Generic;
using Model;
using Xunit;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RepositorioTest
{
    public class CursosRepositorioTest
    {
        [Fact]
        public void GetMateriasTest()
        {
            // Arrange
            var cursosRepositorio = new CursosRepositorio(CreateTestDbContextFunction);
            SeedTestDb(CreateTestDbContextFunction);
            //Act
            var result = cursosRepositorio.GetMaterias(5, 2008);
            //Assert
            Assert.Equal(expected: "Sistemas de Gestión", actual: result.FirstOrDefault().Descripcion);
            Assert.Equal(expected: 2008, actual: result.FirstOrDefault().Plan.Anio);
            Assert.Equal(expected: "Ingeniería en Sistemas de Información", actual: result.FirstOrDefault().Plan.Especialidad.Descripcion);
        }

        private ApplicationContext CreateTestDbContextFunction()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite("Data Source=./academia.db");

            return new ApplicationContext(optionsBuilder.Options);
        }

        private void SeedTestDb(Func<ApplicationContext> contextBuilderFunc)
        {
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
                    Id = 5,
                    Descripcion = "Ingeniería Civil"
                }
            };

            var planes = new List<Plan>()
            {
                new()
                {
                    Anio = 2008,
                    Especialidad = especialidades[0]
                },
                new()
                {
                    Anio = 1995,
                    Especialidad = especialidades[0]
                },
                new()
                {
                    Anio = 1994,
                    Especialidad = especialidades[3]
                },
                new()
                {
                    Anio = 2009,
                    Especialidad = especialidades[4]
                }
            };

            var materias = new List<Materia>
            {
                new()
                {
                    Descripcion = "Sistemas de Gestión",
                    HsSemanales = 4,
                    HsTotales = 136,
                    Plan = planes[0]
                },
                new()
                {
                    Descripcion = "Administración Gerencial",
                    HsSemanales = 6,
                    HsTotales = 102,
                    Plan = planes[0]
                }
            };
            using (var context = contextBuilderFunc())
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();

                context.Especialidades.AddRange(especialidades);
                context.Planes.AddRange(planes);
                context.Materias.AddRange(materias);
                context.SaveChanges();
            }
        }
    }
}
