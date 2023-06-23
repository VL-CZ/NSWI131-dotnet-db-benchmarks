using BenchmarkDotNet.Attributes;
using DbBenchmarks.Common;
using DbBenchmarks.Sqlite;
using DbBenchmarks.SqlServer;
using DbBenchmarks.Workloads;

namespace DbBenchmarks;

static class CommonUtils
{
    // Set either `SqlServerDbFactory` or `SqliteDbFactory`
    public static readonly IDbConnectionFactory DbFactory = new SqlServerDbFactory();
}

[AllStatisticsColumn]
public class GetTop1000ProductsBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetTop1000ProductsBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public List<Product> RawSql()
    {
        return rawSql.GetTop1000Products();
    }

    [Benchmark]
    public List<Product> EfCore()
    {
        return efCore.GetTop1000Products();
    }
}

[AllStatisticsColumn]
public class GetProductByIdBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    private Random random = new Random();

    public GetProductByIdBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public Product RawSql()
    {
        int id = random.Next(1, 1000000);
        return rawSql.GetProductById(id);
    }

    [Benchmark]
    public Product EfCore()
    {
        int id = random.Next(1, 1000000);
        return efCore.GetProductById(id);
    }
}

[AllStatisticsColumn]
public class GetCheapProductsBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetCheapProductsBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public List<Product> RawSql()
    {
        return rawSql.GetCheapProducts();
    }

    [Benchmark]
    public List<Product> EfCore()
    {
        return efCore.GetCheapProducts();
    }
}

[AllStatisticsColumn]
public class GetCountOfCheapProductsBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetCountOfCheapProductsBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public int RawSql()
    {
        return rawSql.GetCountOfCheapProducts();
    }

    [Benchmark]
    public int EfCore()
    {
        return efCore.GetCountOfCheapProducts();
    }
}

[AllStatisticsColumn]
public class GetMinProductPriceBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetMinProductPriceBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public double RawSql()
    {
        return rawSql.GetMinProductPrice();
    }

    [Benchmark]
    public double EfCore()
    {
        return efCore.GetMinProductPrice();
    }
}

[AllStatisticsColumn]
public class GetTop1000ProductNamesBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetTop1000ProductNamesBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public List<string> RawSql()
    {
        return rawSql.GetTop1000ProductNames();
    }

    [Benchmark]
    public List<string> EfCore()
    {
        return efCore.GetTop1000ProductNames();
    }
}

[AllStatisticsColumn]
public class GetTop1000ProductsWithCategories
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetTop1000ProductsWithCategories()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public List<Product> RawSql()
    {
        return rawSql.GetTop1000ProductsWithCategories();
    }

    [Benchmark]
    public List<Product> EfCore()
    {
        return efCore.GetTop1000ProductsWithCategories();
    }
}

[AllStatisticsColumn]
public class GetTop1000OrdersWithAllEntitiesLoadedBenchmark
{
    private IDbWorkload rawSql;
    private IDbWorkload efCore;

    public GetTop1000OrdersWithAllEntitiesLoadedBenchmark()
    {
        var dbFactory = CommonUtils.DbFactory;

        efCore = new EfCoreDbWorkload(dbFactory);
        rawSql = new RawSqlDbWorkload(dbFactory);
    }

    [Benchmark(Baseline = true)]
    public List<Order> RawSql()
    {
        return rawSql.GetTop1000OrdersWithAllEntitiesLoaded();
    }

    [Benchmark]
    public List<Order> EfCore()
    {
        return efCore.GetTop1000OrdersWithAllEntitiesLoaded();
    }
}
