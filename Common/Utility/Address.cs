namespace Common.Utility;
public class Address
{
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }

    public Address() { }

    public Address(string addressLine1, 
                   string addressLine2, 
                   string city, 
                   string state,
                   string zip)
    {
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        City = city;
        State = state;
        Zip = zip;
    }

    public static Address Default => new("", "", "", "", "");
}