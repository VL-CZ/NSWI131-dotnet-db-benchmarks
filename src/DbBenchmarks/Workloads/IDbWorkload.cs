using DbBenchmarks.Common;

namespace DbBenchmarks.Workloads;

internal interface IDbWorkload
{
    /// <summary>
    /// Name of the access method
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Get first 1000 products
    /// </summary>
    /// <returns></returns>
    List<Product> GetTop1000Products();

    /// <summary>
    /// Get product data by its identifier
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Product GetProductById(int id);

    /// <summary>
    /// Add new product
    /// </summary>
    /// <param name="product"></param>
    void AddProduct(Product product);

    /// <summary>
    /// Get products whose price is <= 10 ordered by name
    /// </summary>
    /// <returns></returns>
    List<Product> GetCheapProducts();

    /// <summary>
    /// Get count of products whose price is <= 10
    /// </summary>
    /// <returns></returns>
    int GetCountOfCheapProducts();

    /// <summary>
    /// Get cheapest product price
    /// </summary>
    /// <returns></returns>
    double GetMinProductPrice();

    /// <summary>
    /// Get names of top 1000 products
    /// </summary>
    /// <returns></returns>
    List<string> GetTop1000ProductNames();

    /// <summary>
    /// Get top 1000 categories with `Category` enttities loaded 
    /// </summary>
    /// <returns></returns>
    List<Product> GetTop1000ProductsWithCategories();

    /// <summary>
    /// Get all orders together with 
    /// </summary>
    /// <returns></returns>
    List<Order> GetTop1000OrdersWithAllEntitiesLoaded();

}
