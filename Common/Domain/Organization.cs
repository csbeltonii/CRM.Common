namespace Common.Domain;

public class Organization : Entity
{
    public string Name { get; set; }
    public string Owner { get; set; }
    public Organization(string userId) : base(userId) { }
}