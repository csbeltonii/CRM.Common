namespace Domain.Model;

public class Product : Entity
{
    public decimal Price { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ICollection<ProductProperty> Properties { get; set; }

    public Product()
    {
        Properties = new HashSet<ProductProperty>();
        IsActive = true;
        CreatedDate = DateTime.Now;
    }

    public Product(string id, string name, string description, decimal price, ICollection<ProductProperty> properties)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Properties = properties;
        CreatedDate = DateTime.Now;
        IsActive = true;
    }
}