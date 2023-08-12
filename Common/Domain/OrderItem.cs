namespace Domain.Model;

public class OrderItem : Entity
{
    public string Name { get; set; }
    public string OrderId { get; set; }
    public decimal Price { get; set; }
    public decimal Quantity { get; set; }

    public OrderItem() { }

    public OrderItem(string id, string name, string orderId, decimal price, decimal quantity)
    {
        Id = id;
        Name = name;
        OrderId = orderId;
        Price = price;
        Quantity = quantity;

        CreatedDate = DateTime.Now;
    }
}