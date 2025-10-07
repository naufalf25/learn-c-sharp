namespace SOLID_and_KISS_principles.Models;

public class User
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
        CreatedAt = DateTime.Now;
        IsActive = true;
    }
}

public class OrderItem
{
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal PricePerItem { get; set; }

    public decimal TotalPrice => Quantity * PricePerItem;

    public OrderItem(string productName, int quantity, decimal pricePerItem)
    {
        ProductName = productName;
        Quantity = quantity;
        PricePerItem = pricePerItem;
    }
}

public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public List<OrderItem> Items { get; set; } = new();
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public Order(string customerEmail)
    {
        CustomerEmail = customerEmail;
        OrderDate = DateTime.Now;
        Items = new List<OrderItem>();
    }
}