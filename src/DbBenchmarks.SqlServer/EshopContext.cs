using DbBenchmarks.Common;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;

namespace DbBenchmarks.SqlServer;

public class SqlServerDbFactory : IDbConnectionFactory
{
    public string DbName => "SQL Server";

    public LimitQueryType LimitQuery => LimitQueryType.TopXX;

    public bool CastAggregationResultToLong => false;

    public DbConnection GetConnection()
    {
        return new SqlConnection(SqlServerDbUtils.ConnectionString);
    }

    public IEshopContext GetDbContexxtt()
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
        options.UseSqlServer(SqlServerDbUtils.ConnectionString);
    }


}
