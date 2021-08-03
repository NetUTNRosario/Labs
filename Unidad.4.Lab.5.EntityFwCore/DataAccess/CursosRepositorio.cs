using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    public class CursosRepositorio
    {
        private readonly IApplicationContextFactory _contextFactory;

        public CursosRepositorio(IApplicationContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// Traer las materias con menos de x horas semanales con el plan z ordenados 
        /// en forma descendente por HsSemanales, incluyendo los datos del plan y la 
        /// especialidad asociados a esta
        public IEnumerable<Materia> GetMaterias(int hsSemanales, int anioPlan)
        {
            using (ApplicationContext context = _contextFactory.CreateContext())
            {
                return context.Materias
                    .Include(m => m.Plan).ThenInclude(p => p.Especialidad)
                    .Where(m => m.HsSemanales <= hsSemanales && m.Plan.Anio == anioPlan)
                    .OrderByDescending(m => m.HsSemanales).ToList();
            }
        }

        /// Guardar una materia con el plan mas actual que este asociado con la especialidad
        /// que contenga el nombre parcial enviado como parametro
        public void InsertMateria(Materia materia, string nombreParcialEspecialidad)
        {
            using (ApplicationContext context = _contextFactory.CreateContext())
            {
                var plan = context.Planes
                    .Where(p => p.Especialidad.Descripcion.Contains(nombreParcialEspecialidad))
                    .OrderBy(p => p.Anio)
                    .FirstOrDefault();
                    
                materia.Plan = plan;
                context.Materias.Add(materia);
                context.SaveChanges();
            }
        }
    }
}
