namespace DbBenchmarks;

public class Customer
{
    public Customer() { }

    public Customer(int id, string fullName, string email)
    {
        Id = id;
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

    public Order(int id, OrderStatus status)
    {
        Id = id;
        Status = status;
    }

    public int Id { get; set; }

    public OrderStatus Status { get; set; }

    public Customer Customer { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public Product() { }

    public Product(string name, string description, double price)
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public Product(int id, string name, string description, double price) : this(name, description, price)
    {
        Id = id;
    }

    public Product(int id, string name, string description, double price, Category category) : this(id, name, description, price)
    {
        Category = category;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public Category Category { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}

public class Category
{
    public Category() { }

    public Category(int id, string name)
    {
        (Id, Name) = (id, name);
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
