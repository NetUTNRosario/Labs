using System;
using System.Collections.Generic;
using System.Linq;

namespace FuncionesLINQ
{
    public class FuncionesLinq
    {
        public IEnumerable<string> ObtenerProvinciasQueEmpiezanConDeterminadasLetras()
        {
            IEnumerable<string> provincias = new List<string>
            {
                "Buenos Aires",
                "Capital Federal",
                "Catamarca",
                "Chaco",
                "Chubut",
                "Córdoba",
                "Corrientes",
                "Entre Ríos",
                "Formosa",
                "Jujuy",
                "La Pampa",
                "La Rioja",
                "Mendoza",
                "Misiones",
                "Neuquén",
                "Río Negro",
                "Salta",
                "San Juan",
                "San Luis",
                "Santa Cruz",
                "Santa Fe",
                "Santiago del Estero",
                "Tierra del Fuego, Antártida e Isla del Atlántico Sur",
                "Tucumán"
            };

            return provincias.Where(e => e.StartsWith("T") || e.StartsWith("S"));
        }

        public IEnumerable<int> ObtenerNumerosMayoresA20()
        {
            IEnumerable<int> numeros = new List<int> { 14, 2, 5, 21, 32, 25, 20 };

            return numeros.Where(n => n > 20);
        }

        public IEnumerable<int> ObtenerCodigoPostalDeCiudadesQueTenganEnSuNombreTresCarateresDeterminados(string ciudad)
        {
            IEnumerable<Ciudad> ciudades = new List<Ciudad>
            {
                 new Ciudad() { Nombre = "Rosario", CodigoPostal = 2000 },
                 new Ciudad()
                 {
                     Nombre = "Córdoba",
                     CodigoPostal = 5000
                 },
                new Ciudad()
                {
                    Nombre = "Santa Fe",
                    CodigoPostal = 3000
                },
                new Ciudad() {
                    Nombre = "San Miguel De Tucuman",
                    CodigoPostal = 4000
                }
                // Cargar 6 mas
            };

            string ciudadNormalizada = ciudad.ToLower();

            return ciudades.Where(c => c.Nombre.ToLower().Contains(ciudadNormalizada)).Select(c => c.CodigoPostal);
        }
    }
}
