using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
    /// Debido a la necesidad de hacer visible en tiempo de diseño 
    /// la configuracion del db context a usar para las migraciones
    /// se requiere utilizar esta construicion que la herramienta de 
    /// linea de comandos puede localizar
    class ApplicationContextDesignTimeFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite();

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}