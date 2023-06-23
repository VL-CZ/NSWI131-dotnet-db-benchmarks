using DbBenchmarks.Common;
using DbBenchmarks.SqlServer;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DbBenchmarks.Workloads;
using DbBenchmarks.Sqlite;
using System.Security.AccessControl;

namespace DbBenchmarks;

internal class Program
{

    static void Main(string[] args)
    {
        (int thousand, int million) = (1000, 1000000);

        (int categories, int products, int orders, int customers) = (thousand, million, 100 * thousand, thousand);
        int maxOrderProducts = 10;

        // SQLite
        //var dbConnectionFactory = new SqliteDbFactory();

        // SQL Server
        //var dbConnectionFactory = new SqlServerDbFactory();

        // Generate data
        //TestDataManager.GenerateData(dbConnectionFactory, categories, products, orders, customers, maxOrderProducts);

        // Run benchmarks
        var summary = BenchmarkRunner.Run<GetTop1000ProductsBenchmark>();
        summary = BenchmarkRunner.Run<GetProductByIdBenchmark>();
        summary = BenchmarkRunner.Run<GetCheapProductsBenchmark>();
        summary = BenchmarkRunner.Run<GetCountOfCheapProductsBenchmark>();
        summary = BenchmarkRunner.Run<GetMinProductPriceBenchmark>();
        summary = BenchmarkRunner.Run<GetTop1000ProductNamesBenchmark>();
        summary = BenchmarkRunner.Run<GetTop1000ProductsWithCategories>();
        summary = BenchmarkRunner.Run<GetTop1000OrdersWithAllEntitiesLoadedBenchmark>();
    }
}