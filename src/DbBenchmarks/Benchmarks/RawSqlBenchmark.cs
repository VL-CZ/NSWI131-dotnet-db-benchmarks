using DbBenchmarks.Queries;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Data.Common;

namespace DbBenchmarks.Benchmarks;

internal class RawSqlBenchmark : IDbBenchmark
{
    private DbConnection GetConnection()
    {
        return new SqliteConnection($"Data Source={DbUtils.DbPath}");
    }

    public void AddProduct(Product product)
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)";

        // add SQL params
        var parameter1 = command.CreateParameter();
        parameter1.ParameterName = "@Name";
        parameter1.Value = product.Name;
        command.Parameters.Add(parameter1);

        var parameter2 = command.CreateParameter();
        parameter2.ParameterName = "@Description";
        parameter2.Value = product.Description;
        command.Parameters.Add(parameter2);

        var parameter3 = command.CreateParameter();
        parameter3.ParameterName = "@Price";
        parameter3.Value = product.Price;
        command.Parameters.Add(parameter3);

        _ = command.ExecuteNonQuery();
    }

    public List<Product> GetCheapProducts()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Products WHERE Price <= 10";

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

        var count = (Int64)command.ExecuteScalar();
        return (int)count;
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

    public List<Product> GetTop1000Products()
    {
        using DbConnection connection = GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Products LIMIT 1000";

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
}
