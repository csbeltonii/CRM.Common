using Common.Enums;
using Common.Interfaces;

namespace Common.Domain;

public class Order : Entity, IOrganizationEntity
{
    public static Order Null => new("");

    public string CustomerId { get; set; }
    public string OrganizationId { get; set; }
    public OrderStatus Status { get; set; }
    public bool IsShipped { get; set; }

    private Order(string userId) : base(userId) { }

    public Order(string id, string customerId, string userId) : base(userId)
    {
        Id = id;
        CustomerId = customerId;
    }
}