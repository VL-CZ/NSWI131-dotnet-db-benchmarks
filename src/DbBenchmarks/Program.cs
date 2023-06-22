using DbBenchmarks.Benchmarks;
using DbBenchmarks.Common;
using DbBenchmarks.Sqlite;
using DbBenchmarks.Queries;
using System.Diagnostics;

namespace DbBenchmarks;

static class ListExtensions
{
    public static T GetRandom<T>(this List<T> items)
    {
        return items[Random.Shared.Next(items.Count)];
    }
}

class BenchmarkTool
{
    private Stopwatch stopwatch = new();
    private const int repetitions = 10;

    public void Benchmark(Action method, string name)
    {
        stopwatch.Restart();

        for (int i = 0; i < repetitions; i++)
        {
            method();
        }

        stopwatch.Stop();

        var elapsed = Math.Round(stopwatch.Elapsed.TotalMilliseconds / repetitions, 3);

        Console.WriteLine($"{name}: {elapsed}");
    }
}

internal class Program
{
    static void MeasureData()
    {
        var sqliteEfFactory = new SqliteEshopContextFactory();

        var benchmarkTool = new BenchmarkTool();
        var benchmarks = new IDbBenchmark[] { new RawSqlBenchmark(), new EfCoreBenchmark(sqliteEfFactory) };

        foreach (var benchmark in benchmarks)
        {
            Console.WriteLine($"---------- {benchmark.Name} ----------");

            var product = new Product("Name", "Description", 999.99);
            int productId = 2000;

            var benchmarkMethods = new List<(Action, string)>()
            {
                (() => {_ = benchmark.GetTop1000Products();}, "Get top 1000"),
                (() => {_ = benchmark.GetProductById(productId);}, "Get by ID"),
                (() => {_ = benchmark.GetTop1000ProductNames();}, "Get product names"),
                (() => {_ = benchmark.GetCheapProducts();}, "Get cheap"),
                (() => {_ = benchmark.GetCountOfCheapProducts();}, "Get count of cheap"),
                (() => {_ = benchmark.GetMinProductPrice();}, "Get min price"),
                (() => {_ = benchmark.GetTop1000ProductsWithCategories();}, "Get top 1000 with categories"),
                (() => {_ = benchmark.GetTop1000OrdersWithAllEntitiesLoaded();}, "Get top 1000 with all related entities"),
                //(() => { benchmark.AddProduct(product);}, "Add product")
            };

            foreach ((var method, var name) in benchmarkMethods)
            {
                benchmarkTool.Benchmark(method, name);
            }
        }
    }

    static void Main(string[] args)
    {
        (int thousand, int million) = (1000, 1000000);

        (int categories, int products, int orders, int customers) = (thousand, million, 100 * thousand, thousand);
        int maxOrderProducts = 10;

        // SQLite
        //var eshopSqliteFactory = new SqliteEshopContextFactory();
        //TestDataManager.GenerateData(eshopSqliteFactory, categories, products, orders, customers, maxOrderProducts);

        // SQL Server

        MeasureData();

        //TestDataManager.DeleteData();
    }
}