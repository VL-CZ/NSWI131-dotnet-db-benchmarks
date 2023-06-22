using Microsoft.EntityFrameworkCore;

namespace DbBenchmarks.Common;

public interface IEshopContext : IDisposable
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Category> Categories { get; set; }
    int SaveChanges();
}

public interface IEshopContextFactory
{
    IEshopContext GetInstance();
}