namespace Domain.Model;

public class Order : Entity
{
    public string CustomerId { get; set; }
    public ICollection<OrderItem> OrderItems  { get; set; }
    public bool IsShipped { get; set; }
    public decimal Tax { get; set; }
    public decimal Total => OrderItems.Sum(item => item.Price);

    public Order()
    {
        OrderItems = new HashSet<OrderItem>();

        CreatedDate = DateTime.Now;
    }

    public Order(string id, string customerId)
    {
        Id = id;
        CustomerId = customerId;

        OrderItems = new HashSet<OrderItem>();
        CreatedDate = DateTime.Now;
    }
}