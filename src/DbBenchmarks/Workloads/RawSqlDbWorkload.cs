using DbBenchmarks.Common;
using System.Data;
using System.Data.Common;

namespace DbBenchmarks.Workloads;

internal class RawSqlDbWorkload : IDbWorkload
{
    private IDbConnectionFactory dbConnectionFactory;
    private readonly string top1000Query;
    private readonly string limit1000Query;

    public RawSqlDbWorkload(IDbConnectionFactory dbConnectionFactory)
    {
        this.dbConnectionFactory = dbConnectionFactory;

        if (dbConnectionFactory.LimitQuery == LimitQueryType.TopXX)
        {
            (top1000Query, limit1000Query) = ("TOP 1000", "");
        }
        else
        {
            (top1000Query, limit1000Query) = ("", "LIMIT 1000");
        }
    }

    private DbConnection GetConnection()
    {
        return dbConnectionFactory.GetConnection();
    }

    public string Name { get => "Raw SQL Benchmark"; }

    public List<Product> GetCheapProducts()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Products WHERE Price <= 10 ORDER BY Name";

        using var reader = command.ExecuteReader();

        var products = new List<Product>();

        while (reader.Read())
        {
            (int id, string name, string description, double price) = (reader.GetInt32("Id"), reader.GetString("Name"),
                reader.GetString("Description"), reader.GetDouble("Price"));

            products.Add(new Product(id, name, description, price));
        }
        return products;
    }

    public int GetCountOfCheapProducts()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT COUNT(*) FROM Products WHERE Price <= 10";

        if (dbConnectionFactory.CastAggregationResultToLong)
        {
            long count = (long)command.ExecuteScalar();
            return (int)count;
        }
        else
        {
            return (int)command.ExecuteScalar();
        }

    }

    public Product GetProductById(int id)
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Products WHERE Id=@Id";

        var p = command.CreateParameter();
        p.ParameterName = "@Id";
        p.Value = id;
        command.Parameters.Add(p);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            (int productId, string name, string description, double price) = (reader.GetInt32("Id"), reader.GetString("Name"),
                reader.GetString("Description"), reader.GetDouble("Price"));

            return new Product(productId, name, description, price);
        }
        throw new ArgumentException($"Product with Id={id} doesn't exist.");
    }

    public List<Product> GetTop1000ProductsWithCategories()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @$"SELECT {top1000Query} p.Id AS ProductId, p.Name AS ProductName, p.Description, p.Price, c.Id AS CategoryId, c.Name as CategoryName 
                                FROM Products p JOIN Categories c ON p.CategoryId = c.Id {limit1000Query}";

        using var reader = command.ExecuteReader();

        var products = new List<Product>();
        var categoriesCreated = new Dictionary<int, Category>();

        while (reader.Read())
        {
            (int pId, string pName, string pDescription, double pPrice, int cId, string cName) = (reader.GetInt32("ProductId"), reader.GetString("ProductName"),
                reader.GetString("Description"), reader.GetDouble("Price"), reader.GetInt32("CategoryId"), reader.GetString("CategoryName"));

            if (!categoriesCreated.TryGetValue(cId, out Category category))
            {
                category = new Category(cId, cName);
                categoriesCreated[cId] = category;
            }

            var product = new Product(pId, pName, pDescription, pPrice, category);

            category.Products.Add(product);
            products.Add(product);
        }
        return products;
    }

    public List<Product> GetTop1000Products()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = $"SELECT {top1000Query} * FROM Products {limit1000Query}";

        using var reader = command.ExecuteReader();

        var products = new List<Product>();

        while (reader.Read())
        {
            (int id, string name, string description, double price) = (reader.GetInt32("Id"), reader.GetString("Name"),
                reader.GetString("Description"), reader.GetDouble("Price"));

            products.Add(new Product(id, name, description, price));
        }
        return products;
    }

    public List<string> GetTop1000ProductNames()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = $"SELECT {top1000Query} Name FROM Products {limit1000Query}";

        using var reader = command.ExecuteReader();

        var categoryNames = new List<string>();

        while (reader.Read())
        {
            string name = reader.GetString("Name");

            categoryNames.Add(name);
        }
        return categoryNames;
    }

    public double GetMinProductPrice()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT MIN(Price) FROM Products";

        double minPrice = (double)command.ExecuteScalar();
        return minPrice;
    }

    public List<Order> GetTop1000OrdersWithAllEntitiesLoaded()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @$"SELECT o.Status, o.Id as OrderId,
                                    c.FullName, c.Id AS CustomerId, c.Email, 
                                    p.Id AS ProductId, p.Name AS ProductName, p.Description, p.Price, 
                                    ca.Id AS CategoryId, ca.Name as CategoryName 
                                FROM (
                                    SELECT {top1000Query} * FROM Orders {limit1000Query}
                                ) AS o
                                INNER JOIN Customers c on c.Id = o.CustomerId
                                INNER JOIN OrderProduct op on o.Id = op.OrdersId
                                INNER JOIN Products p on op.ProductsId = p.Id
                                INNER JOIN Categories ca on ca.Id = p.CategoryId 
                                ";

        using var reader = command.ExecuteReader();

        var ordersCreated = new Dictionary<int, Order>();
        var categoriesCreated = new Dictionary<int, Category>();
        var customersCreated = new Dictionary<int, Customer>();
        var productsCreated = new Dictionary<int, Product>();

        while (reader.Read())
        {
            (int oId, OrderStatus oStatus) = (reader.GetInt32("OrderId"), (OrderStatus)reader.GetInt32("Status"));
            (int cuId, string cuFullname, string cuEmail) = (reader.GetInt32("CustomerId"), reader.GetString("FullName"), reader.GetString("Email"));
            (int pId, string pName, string pDescription, double pPrice) = (reader.GetInt32("ProductId"), reader.GetString("ProductName"),
                reader.GetString("Description"), reader.GetDouble("Price"));
            (int catId, string catName) = (reader.GetInt32("CategoryId"), reader.GetString("CategoryName"));

            // check if the customer hasn't been created yet
            if (!customersCreated.TryGetValue(cuId, out Customer customer))
            {
                customer = new Customer(cuId, cuFullname, cuEmail);
                customersCreated[cuId] = customer;
            }

            // check if the order hasn't been created yet
            if (!ordersCreated.TryGetValue(oId, out Order order))
            {
                order = new Order(oId, oStatus);
                ordersCreated[oId] = order;

                order.Customer = customer;
                customer.Orders.Add(order);
            }

            // check if the category hasn't been created yet
            if (!categoriesCreated.TryGetValue(catId, out Category category))
            {
                category = new Category(catId, catName);
                categoriesCreated[catId] = category;
            }

            // check if the product hasn't been created yet
            if (!productsCreated.TryGetValue(pId, out Product product))
            {
                product = new Product(pId, pName, pDescription, pPrice);
                productsCreated[pId] = product;

                product.Category = category;
                category.Products.Add(product);
            }

            order.Products.Add(product);
            product.Orders.Add(order);
        }
        return ordersCreated.Values.ToList();
    }
}
