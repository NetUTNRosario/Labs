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
        [Fact]
        public void GetMateriasTest()
        {
            //Arrange
            var cursosRepositorio = new CursosRepositorio(new TestApplicationContextFactory());
            TestDbSeed.Seed(new TestApplicationContextFactory());
            //Act
            var result = cursosRepositorio.GetMaterias(5, 2008);
            //Assert
            Assert.Equal(expected: "Sistemas de Gestión", actual: result.FirstOrDefault().Descripcion);
            Assert.Equal(expected: 2008, actual: result.FirstOrDefault().Plan.Anio);
            Assert.Equal(expected: "Ingeniería en Sistemas de Información", actual: result.FirstOrDefault().Plan.Especialidad.Descripcion);
        }

        [Fact]
        public void InsertMateriaTest()
        {
            // Arrange
            var cursosRepositorio = new CursosRepositorio(new TestApplicationContextFactory());
            TestDbSeed.Seed(new TestApplicationContextFactory());
            var materia = new Materia()
            {
                Descripcion = "Mineria de Datos",
                HsSemanales = 4,
                HsTotales = 128
            };
            // Act
            cursosRepositorio.InsertMateria(materia, "Ingeniería en Sistemas");
            // Assert
            using (var context = new TestApplicationContextFactory().CreateContext())
            {
                var materiaInDb = context.Materias
                        .Include(m => m.Plan)
                        .ThenInclude(p => p.Especialidad)
                        .FirstOrDefault(m => m.Descripcion == materia.Descripcion);
                Assert.Equal(expected: 4, actual: materiaInDb.HsSemanales);
                Assert.Equal(expected: 128, actual: materiaInDb.HsTotales);
                Assert.Equal(expected: "Ingeniería en Sistemas de Información",
                             actual: materiaInDb.Plan.Especialidad.Descripcion);
            }
        }
    }
}
