using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    // Crear las propiedades DbSet (las cuales representan tablas en la db) 
    // para cada entidad del dominio (Materia, Planes y Especialidades).
    // Luego crear constructor que tome DbContextOptions<ApplicationContext>
    // como parametro y lo envié al constructor de la clase padre DbContext
    public class ApplicationContext : DbContext
    {
        
    }
}