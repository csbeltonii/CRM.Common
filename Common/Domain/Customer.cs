using System.Text.Json.Serialization;
using Common.Utility;
using Domain.Model;
using Domain.Utility;

namespace Common.Domain;

public class Customer : Entity
{
    public string UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public Email Email { get; set; }

    [JsonPropertyName("phoneNumber")] 
    public Phone Phone { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; }

    public Customer() { }

    public Customer(string id, 
                    string userId,
                    string organizationId,
                    string firstName, 
                    string lastName,
                    Phone phoneNumber)
    {
        Id = id;
        UserId = userId;
        OrganizationId = organizationId;
        FirstName = firstName;
        LastName = lastName;
        Phone = phoneNumber;
        CreatedDate = DateTime.UtcNow;
    }
}
