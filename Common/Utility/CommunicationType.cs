namespace Domain.Utility
{
    public class CommunicationType
    {
        public string Value { get; }

        private CommunicationType(string value)
        {
            Value = value;
        }

        public static CommunicationType Primary => new("Primary");
        public static CommunicationType Work => new("Work");
        public static CommunicationType Home => new("Home");
        public static CommunicationType Cell => new("Cell");
        public static CommunicationType Other => new("Other");
    }
}
