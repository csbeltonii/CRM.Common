namespace Domain.Utility
{
    public class Email
    {
        public string Address { get; set; }

        public Email() { }
        public Email(string address)
        {
            Address = address;
        }

        public static Email Default => new("");

        public override string ToString() => Address;
    }
}
