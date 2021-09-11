using System;
using System.Collections.Generic;
using Model;
using Xunit;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccessTest
{
    public class CursosRepositorioTest
    {
        private readonly CursosRepositorio _cursosRepositorio;
        public CursosRepositorioTest()
        {
            // Esto funciona antes de cada test, por lo que la db sqlite es recreada gracias a lo cual los test permanecen aislados entre si
            // This works before each test, so the sqlite db is recreated in order to keep test isolated
            _cursosRepositorio = new CursosRepositorio(new TestApplicationContextFactory());
            TestDbSeed.RecreateAndSeed(new TestApplicationContextFactory());
        }

        [Fact]
        public void GetMateriasTest()
        {
            //Act
            IEnumerable<Materia> result = _cursosRepositorio.GetMaterias(hsSemanales: 5, anioPlan: 2008);
            //Assert
            Assert.Equal(expected: "Sistemas de Gestión", actual: result.FirstOrDefault().Descripcion);
            Assert.Equal(expected: 2008, actual: result.FirstOrDefault().Plan.Anio);
            Assert.Equal(expected: "Ingeniería en Sistemas de Información", actual: result.FirstOrDefault().Plan.Especialidad.Descripcion);
            Assert.Single(result);
        }

        [Fact]
        public void InsertMateriaTest()
        {
            // Arrange
            var materia = new Materia()
            {
                Descripcion = "Mineria de Datos",
                HsSemanales = 4,
                HsTotales = 128
            };
            // Act
            _cursosRepositorio.InsertMateria(materia, nombreParcialEspecialidad: "Ingeniería en Sistemas");
            // Assert
            using (var context = new TestApplicationContextFactory().CreateContext())
            {
                Materia materiaInDb = context.Materias
                        .Include(m => m.Plan)
                        .ThenInclude(p => p.Especialidad)
                        .FirstOrDefault(m => m.Descripcion == materia.Descripcion);
                Assert.Equal(expected: materia.HsSemanales, actual: materiaInDb.HsSemanales);
                Assert.Equal(expected: materia.HsTotales, actual: materiaInDb.HsTotales);
                Assert.Equal(expected: "Ingeniería en Sistemas de Información",
                             actual: materiaInDb.Plan.Especialidad.Descripcion);
            }
        }
    }
}
