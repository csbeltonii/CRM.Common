namespace Common.Domain;

public class OrderItem : Entity
{
    public static OrderItem Null => new("");

    public string Name { get; set; }
    public string OrderId { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }

    private OrderItem(string userId): base(userId) { }

    public OrderItem(
        string id, 
        string name,
        string orderId,
        decimal price,
        decimal quantity, 
        string userId) 
        : base(userId)
    {
        Id = id;
        Name = name;
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
    }
}