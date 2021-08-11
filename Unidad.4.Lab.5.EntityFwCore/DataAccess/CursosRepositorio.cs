using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    public class CursosRepositorio
    {
        public CursosRepositorio(IApplicationContextFactory contextFactory) {}

        /// Traer las materias con menos de x horas semanales con el plan z ordenados 
        /// en forma descendente por HsSemanales, incluyendo los datos del plan y la 
        /// especialidad asociados a esta
        public IEnumerable<Materia> GetMaterias(int hsSemanales, int anioPlan)
        {
            throw new NotImplementedException();
        }

        /// Guardar una materia con el plan mas actual que este asociado con la especialidad
        /// que contenga el nombre parcial enviado como parametro
        public void InsertMateria(Materia materia, string nombreParcialEspecialidad)
        {
            throw new NotImplementedException();
        }
    }
}
