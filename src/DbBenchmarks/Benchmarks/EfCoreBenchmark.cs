using DbBenchmarks.Common;
using DbBenchmarks.Queries;
using Microsoft.EntityFrameworkCore;

namespace DbBenchmarks.Benchmarks;

internal class EfCoreBenchmark : IDbBenchmark
{
    private IDbConnectionFactory eshopContextFactory;

    public EfCoreBenchmark(IDbConnectionFactory eshopContextFactory)
    {
        this.eshopContextFactory = eshopContextFactory;
    }

    public string Name { get => "EF Core Benchmark"; }

    public void AddProduct(Product product)
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        dbContext.Products.Add(product);

        dbContext.SaveChanges();
    }

    public List<Product> GetTop1000Products()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Take(1000).ToList();
    }

    public Product GetProductById(int id)
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Find(id);
    }

    public int GetCountOfCheapProducts()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Count(p => p.Price <= 10);
    }

    public List<Product> GetCheapProducts()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Where(p => p.Price <= 10).OrderBy(p => p.Name).ToList();
    }

    public List<Product> GetTop1000ProductsWithCategories()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Include(p => p.Category).Take(1000).ToList();
    }

    public List<string> GetTop1000ProductNames()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Take(1000).Select(c => c.Name).ToList();
    }

    public double GetMinProductPrice()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Products.Min(p => p.Price);
    }

    public List<Order> GetTop1000OrdersWithAllEntitiesLoaded()
    {
        using var dbContext = eshopContextFactory.GetDbContexxtt();

        return dbContext.Orders.Include(o => o.Customer)
            .Include(o => o.Products).ThenInclude(p => p.Category)
            .Take(1000).ToList();
    }
}
