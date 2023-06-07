namespace DbBenchmarks;

internal static class TestDataGenerator
{
    public static void GenerateData()
    {
        using (var eshopContext = new EshopContext())
        {
            var category = new Category("C1");
            var product = new Product("P1");
            var order = new Order(OrderStatus.Created);
            var customer = new Customer("Jan Novak", "jn@gmail.com");

            eshopContext.Categories.Add(category);
            eshopContext.Products.Add(product);
            eshopContext.Orders.Add(order);
            eshopContext.Customers.Add(customer);

            category.Products.Add(product);
            order.Products.Add(product);
            customer.Orders.Add(order);

            eshopContext.SaveChanges();
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        TestDataGenerator.GenerateData();
    }
}