using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DbBenchmarks.Common;

public interface IEshopContext : IDisposable
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    int SaveChanges();
}

public enum LimitQueryType { LimitXX, TopXX }

public interface IDbConnectionFactory
{
    IEshopContext GetDbContext();

    DbConnection GetConnection();

    string DbName { get; }

    LimitQueryType LimitQuery { get; }

    bool CastAggregationResultToLong { get; }

}