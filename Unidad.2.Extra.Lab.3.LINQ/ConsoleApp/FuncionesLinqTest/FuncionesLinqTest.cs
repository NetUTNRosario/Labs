using FuncionesLINQ;
using System;
using System.Collections.Generic;
using Xunit;

namespace FuncionesLinqTest
{
    public class FuncionesLinqTest
    {
        [Fact]
        public void ObtenerProvinciasQueEmpiezanConDetermindasLetrasTest()
        {
            var funcionesLinq = new FuncionesLinq();

            IEnumerable<string> provinciasSeleccionadas = funcionesLinq.ObtenerProvinciasQueEmpiezanConDeterminadasLetras();

            Assert.Equal(expected: new List<String>
            {
                "Salta",
                "San Juan",
                "San Luis",
                "Santa Cruz",
                "Santa Fe",
                "Santiago del Estero",
                "Tierra del Fuego, Antártida e Isla del Atlántico Sur",
                "Tucumán",
            }, actual: provinciasSeleccionadas);
        }
    }
}
