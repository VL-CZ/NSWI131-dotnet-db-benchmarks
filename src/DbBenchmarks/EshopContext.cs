using Microsoft.EntityFrameworkCore;

namespace DbBenchmarks;

public class EshopContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    public string DbPath { get; }

    public string DbFileName { get; } = "db.sqlite3";

    public EshopContext()
    {
        var binFolder = AppDomain.CurrentDomain.BaseDirectory;
        var rootFolder = Directory.GetParent(binFolder).Parent.Parent.Parent.Parent.Parent.FullName;

        DbPath = Path.Join(rootFolder, "db", DbFileName);
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}