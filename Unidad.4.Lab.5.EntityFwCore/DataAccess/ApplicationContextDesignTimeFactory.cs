using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
    // Debido a la necesidad de hacer visible en tiempo de diseño 
    // la configuración del db context a usar para las migraciones
    // se requiere utilizar esta construcción que la herramienta de 
    // linea de comandos puede localizar
    class ApplicationContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        // En método de la interfaz IDesignTimeDbContextFactory construir un instancia de 
        // DbContextOptionsBuilder<ApplicationContext> para luego usar uno de sus metodos
        // que indica que se va a usar sqlite (UseSqlite(), para las migraciones solo es necesario saber que db se va a utilizar, 
        // por lo que no es necesaria el uso de la connection string), retornar una nueva instancia de ApplicationContext
        // enviando esta el optionsBuilder como parametro. 
        public ApplicationContext CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}