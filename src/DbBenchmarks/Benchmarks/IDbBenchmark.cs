namespace DbBenchmarks.Queries;

internal interface IDbBenchmark
{
    List<Product> GetTop1000Products();

    Product GetProductById(int id);

    void AddProduct(Product product);

    void GetProductsCondition();

    int GetCountOfCheapProducts();

    List<Product> GetCheapProducts();

    List<Product> GetProductsWithCategories();

    List<Product> GetProductsWithOrdersAndCustomers();

}
