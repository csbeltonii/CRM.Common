using Common.Interfaces;

namespace Common.Domain;

public class OrderList : Entity, IOrganizationEntity
{
    public static OrderList Null => new("");

    public string Name { get; set; }
    public string OrderId { get; set; }
    public string OrganizationId { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public decimal Total => OrderItems.Sum(item => item.Price);
    public decimal Tax { get; set; }

    private OrderList(string userId): base(userId) { }

    public OrderList(
        string id, 
        string name,
        string orderId,
        string userId) 
        : base(userId)
    {
        Id = id;
        Name = name;
        OrderId = orderId;
        OrderItems = new HashSet<OrderItem>();
    }
}

public class OrderItem
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }
    
}