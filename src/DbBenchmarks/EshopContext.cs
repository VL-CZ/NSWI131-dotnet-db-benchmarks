using Microsoft.EntityFrameworkCore;

namespace DbBenchmarks;

public class EshopContext : DbContext
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
        // SQLite
        options.UseSqlite($"Data Source={SqliteDbUtils.DbPath}");

        // SQL Server
    }
}