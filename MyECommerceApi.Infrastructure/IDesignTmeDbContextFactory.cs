using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyECommerceApi.Infrastructure
{
    /// <summary>
    /// This will resolve dbcontext instantiation issue at design time, (because program.cs file which carry the db connection details is call at runtime)
    /// </summary>
    public class MyECommerceApiDbContextFactory : IDesignTimeDbContextFactory<MyECommerceApiDbContext>
    {
        public MyECommerceApiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyECommerceApiDbContext>();
            optionsBuilder.UseSqlServer("Server=LP502;Database=ECommerce;Trusted_Connection=True;TrustServerCertificate=True;");
            return new MyECommerceApiDbContext(optionsBuilder.Options);
        }
    }
}