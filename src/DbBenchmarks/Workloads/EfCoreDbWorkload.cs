using DbBenchmarks.Common;
using Microsoft.EntityFrameworkCore;

namespace DbBenchmarks.Workloads;

internal class EfCoreDbWorkload : IDbWorkload
{
    private IDbConnectionFactory dbConnectionFactory;

    public EfCoreDbWorkload(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;
    }

    public string Name { get => "EF Core Benchmark"; }

    public void AddProduct(Product product)
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        dbContext.Products.Add(product);

        dbContext.SaveChanges();
    }

    public List<Product> GetTop1000Products()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Take(1000).ToList();
    }

    public Product GetProductById(int id)
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Find(id);
    }

    public int GetCountOfCheapProducts()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Count(p => p.Price <= 10);
    }

    public List<Product> GetCheapProducts()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Where(p => p.Price <= 10).OrderBy(p => p.Name).ToList();
    }

    public List<Product> GetTop1000ProductsWithCategories()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Include(p => p.Category).Take(1000).ToList();
    }

    public List<string> GetTop1000ProductNames()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Take(1000).Select(c => c.Name).ToList();
    }

    public double GetMinProductPrice()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Products.Min(p => p.Price);
    }

    public List<Order> GetTop1000OrdersWithAllEntitiesLoaded()
    {
        using var dbContext = dbConnectionFactory.GetDbContext();

        return dbContext.Orders.Include(o => o.Customer)
            .Include(o => o.Products).ThenInclude(p => p.Category)
            .Take(1000).ToList();
    }
}
