namespace Common.Domain;

public class Inventory : Entity
{
    public static Inventory Null => new("");

    public string ProductId { get; set; }
    public decimal QuantityOnHand { get; set; }

    private Inventory(string userId) : base(userId) { }

    public Inventory(
        string productId,
        decimal quantityOnHand,
        string organizationId,
        string userId) 
        : base(userId)
    {
        ProductId = productId;
        QuantityOnHand = quantityOnHand;
        OrganizationId = organizationId;
    }
}