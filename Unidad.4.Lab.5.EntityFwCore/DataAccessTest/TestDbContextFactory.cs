using DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessTest
{
    public class TestApplicationContextFactory : IApplicationContextFactory
    {
        public ApplicationContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseSqlite("Data Source=./academia.db");

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}