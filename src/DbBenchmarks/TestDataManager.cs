using Bogus;
using DbBenchmarks.Common;

namespace DbBenchmarks;

internal static class TestDataManager
{
    public static void GenerateData(IDbConnectionFactory eshopContextFactory, int categoryCount, int productCount, int orderCount, int customerCount, int maxOrderProducts)
    {
        // generate fake testing data

        var categoryFaker = new Faker<Category>()
            .RuleFor(c => c.Name, f => f.Lorem.Text());

        var productFaker = new Faker<Product>()
            .RuleFor(p => p.Name, f => f.Commerce.ProductName())
            .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
            .RuleFor(p => p.Price, f => (double)f.Finance.Amount());

        var orderFaker = new Faker<Order>()
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>());

        var customerFaker = new Faker<Customer>()
            .RuleFor(c => c.FullName, f => f.Name.FullName())
            .RuleFor(c => c.Email, f => f.Internet.Email());

        var categories = categoryFaker.Generate(categoryCount);
        var products = productFaker.Generate(productCount);
        var orders = orderFaker.Generate(orderCount);
        var customers = customerFaker.Generate(customerCount);

        // store the data

        using var eshopContext = eshopContextFactory.GetDbContexxtt();

        eshopContext.Categories.AddRange(categories);
        eshopContext.Products.AddRange(products);
        eshopContext.Orders.AddRange(orders);
        eshopContext.Customers.AddRange(customers);

        // create relations

        foreach (var p in products)
        {
            p.Category = categories.GetRandom();
        }

        foreach (var o in orders)
        {
            o.Customer = customers.GetRandom();
        }

        foreach (var o in orders)
        {
            int orderProductCount = Random.Shared.Next(1, maxOrderProducts);

            for (int i = 0; i < orderProductCount; i++)
            {
                o.Products.Add(products.GetRandom());
            }
        }

        eshopContext.SaveChanges();
    }

    //public static void DeleteData()
    //{
    //    using var eshopContext = new EshopContext();

    //    eshopContext.Categories.RemoveRange(eshopContext.Categories);
    //    eshopContext.Products.RemoveRange(eshopContext.Products);
    //    eshopContext.Orders.RemoveRange(eshopContext.Orders);
    //    eshopContext.Customers.RemoveRange(eshopContext.Customers);

    //    eshopContext.SaveChanges();
    //}
}
