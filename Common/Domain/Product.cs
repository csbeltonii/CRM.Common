namespace Common.Domain;

public class Product : Entity
{
    public static Product Null => new("");

    public decimal Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ProductProperty> Properties { get; set; }

    private Product(string userId): base(userId)
    {
        Properties = new HashSet<ProductProperty>();
        IsActive = true;
    }

    public Product(
        string id,
        string name, 
        string description, 
        decimal price, 
        ICollection<ProductProperty> properties, 
        string userId) 
        : base(userId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Properties = properties;
        IsActive = true;
    }
}