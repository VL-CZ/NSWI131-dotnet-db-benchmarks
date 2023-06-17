using DbBenchmarks.Queries;

namespace DbBenchmarks.Benchmarks;

internal class EfCoreBenchmark : IDbBenchmark
{
    public void AddProduct(Product product)
    {
        using var dbContext = new EshopContext();

        dbContext.Products.Add(product);

        dbContext.SaveChanges();
    }

    public List<Product> GetTop1000Products()
    {
        using var dbContext = new EshopContext();

        return dbContext.Products.Take(1000).ToList();
    }

    public Product GetProductById(int id)
    {
        using var dbContext = new EshopContext();

        return dbContext.Products.Find(id);
    }

    public int GetCountOfCheapProducts()
    {
        using var dbContext = new EshopContext();

        return dbContext.Products.Count(p => p.Price <= 10);
    }

    public List<Product> GetCheapProducts()
    {
        using var dbContext = new EshopContext();

        return dbContext.Products.Where(p => p.Price <= 10).ToList();
    }

    public void GetProductsCondition()
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProductsWithCategories()
    {
        throw new NotImplementedException();
    }

    public List<Product> GetProductsWithOrdersAndCustomers()
    {
        throw new NotImplementedException();
    }
}
