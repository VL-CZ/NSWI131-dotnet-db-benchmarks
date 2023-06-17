using DbBenchmarks.Benchmarks;
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

internal class Program
{
    static void MeasureData()
    {
        var benchmarks = new IDbBenchmark[] { new RawSqlBenchmark(), new EfCoreBenchmark() };

        var stopwatch = new Stopwatch();

        foreach (var benchmark in benchmarks)
        {
            Product p = new Product("Name", "Description", 999.99);

            stopwatch.Restart();

            _ = benchmark.GetTop1000Products();

            stopwatch.Stop();
            Console.WriteLine($"Get top 1000: {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();

            benchmark.AddProduct(p);

            stopwatch.Stop();
            Console.WriteLine($"Add: {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();

            benchmark.GetProductById(2000);

            stopwatch.Stop();
            Console.WriteLine($"Get by ID: {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();

            _ = benchmark.GetCheapProducts();

            stopwatch.Stop();
            Console.WriteLine($"Get cheap: {stopwatch.ElapsedMilliseconds}");

            stopwatch.Restart();

            _ = benchmark.GetCountOfCheapProducts();

            stopwatch.Stop();
            Console.WriteLine($"Get count: {stopwatch.ElapsedMilliseconds}");
        }
    }

    static void Main(string[] args)
    {
        (int thousand, int million) = (1000, 1000000);

        (int categories, int products, int orders, int customers) = (thousand, million, 100 * thousand, thousand);
        int maxOrderProducts = 10;

        //TestDataManager.GenerateData(categories, products, orders, customers, maxOrderProducts);

        MeasureData();

        //TestDataManager.DeleteData();
    }
}