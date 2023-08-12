namespace Domain.Model;

public class Inventory : Entity
{
    public string ProductId { get; set; }
    public decimal QuantityOnHand { get; set; }


    public Inventory()
    {
        CreatedDate = DateTime.UtcNow;
    }

    public Inventory(string productId, decimal quantityOnHand, string organizationId)
    {
        ProductId = productId;
        QuantityOnHand = quantityOnHand;
        OrganizationId = organizationId;
        CreatedDate = DateTime.Now;
    }
}