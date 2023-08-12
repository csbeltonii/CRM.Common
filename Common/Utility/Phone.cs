namespace Domain.Utility;

public class Phone
{
    public string Number { get; set; }

    public Phone() { }
    public Phone(string number)
    {
        Number = number;
    }

    public override string ToString() => Number;
}