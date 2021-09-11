using System.Collections.Generic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccessTest {
    public class TestDbSeed
    {
        public static void RecreateAndSeed(IApplicationContextFactory contextFactory)
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
            using (ApplicationContext context = contextFactory.CreateContext())
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