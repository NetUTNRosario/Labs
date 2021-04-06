using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncionesLINQ
{
    public class FuncionesLinq
    {
        public IEnumerable<string> ObtenerProvinciasQueEmpiezanConDeterminadasLetras(IEnumerable<string> provincias)
        {
            return provincias.Where(e => e.StartsWith("T") || e.StartsWith("S"));
        }

        public IEnumerable<int> ObtenerNumerosMayoresA20(IEnumerable<int> numeros)
        {
            return numeros.Where(n => n > 20);
        }

        public IEnumerable<int> ObtenerCodigoPostalDeCiudadesQueTenganEnSuNombreTresCarateresDeterminados(IEnumerable<Ciudad> ciudades, string ciudad)
        {
            string ciudadNormalizada = ciudad.ToLower();

            return ciudades.Where(c => c.Nombre.ToLower().Contains(ciudadNormalizada)).Select(c => c.CodigoPostal);
        }

        public IEnumerable<Empleado> AgregarEmpleadoListaDevolviendolaOrdenadaPorSueldo(IEnumerable<Empleado> empleados, IEnumerable<Empleado> empleadosParaAgregar, string order)
        {
            var empleadosActualizados = empleados.Union(empleadosParaAgregar);

            return order == "ASC" ? empleadosActualizados.OrderBy(e => e.Sueldo) : empleadosActualizados.OrderByDescending(e => e.Sueldo);
        }
    }
}
