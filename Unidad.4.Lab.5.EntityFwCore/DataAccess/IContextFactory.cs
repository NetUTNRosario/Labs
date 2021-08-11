namespace DataAccess
{
    public interface IApplicationContextFactory
    {
        ApplicationContext CreateContext();
    }
}