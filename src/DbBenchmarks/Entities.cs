namespace DbBenchmarks;

public class Customer
{
    public Customer() { }

    public Customer(string fullName, string email)
    {
        FullName = fullName;
        Email = email;
    }

    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public enum OrderStatus
{
    Created,
    Processing,
    Completed
}

public class Order
{
    public Order() { }

    public Order(OrderStatus status)
    {
        Status = status;
    }

    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    public Customer Customer { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public Product () { }

    public Product(string name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public Category Category { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class Category
{
    public Category() { }

    public Category(string name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
