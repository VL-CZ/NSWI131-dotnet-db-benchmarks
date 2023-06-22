using DbBenchmarks.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DbBenchmarks.Sqlite;

public class SqliteDbFactory : IDbConnectionFactory
{
    public string DbName => "SQLite";

    public LimitQueryType LimitQuery => LimitQueryType.LimitXX;

    public bool CastAggregationResultToLong => true;

    public IEshopContext GetDbContext()
    {
        return new EshopContext();
    }

    public DbConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={SqliteDbUtils.DbPath}");
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
        options.UseSqlite($"Data Source={SqliteDbUtils.DbPath}");
    }


}
