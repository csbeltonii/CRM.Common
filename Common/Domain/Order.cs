namespace Common.Domain;

public class Order : Entity
{
    public static Order Null => new("");

    public string CustomerId { get; set; }
    public ICollection<OrderItem> OrderItems  { get; set; }
    public bool IsShipped { get; set; }
    public decimal Tax { get; set; }
    public decimal Total => OrderItems.Sum(item => item.Price);

    private Order(string userId) : base(userId)
    {
        OrderItems = new HashSet<OrderItem>();
    }

    public Order(string id, string customerId, string userId) : base(userId)
    {
        Id = id;
        CustomerId = customerId;
        OrderItems = new HashSet<OrderItem>();
    }
}