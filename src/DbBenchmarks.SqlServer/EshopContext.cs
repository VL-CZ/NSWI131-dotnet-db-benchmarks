using DbBenchmarks.Common;
using Microsoft.EntityFrameworkCore;

namespace DbBenchmarks.SqlServer;

public class SqliteEshopContextFactory : IEshopContextFactory
{
    public IEshopContext GetInstance()
    {
        return new EshopContext();
    }
}

public class EshopContext : DbContext, IEshopContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public EshopContext()
    {
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // SQL Server
        options.UseSqlServer("Server=localhost,5555;Database=EshopDb;User Id=sa;Password=Password01.;TrustServerCertificate=True");
    }


}
