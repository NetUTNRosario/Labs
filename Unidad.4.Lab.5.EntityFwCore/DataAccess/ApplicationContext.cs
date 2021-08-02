using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> contextOptions): base(contextOptions) { }
        public DbSet<Especialidad> Especialidades { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Materia> Materias { get; set; }
    }
}