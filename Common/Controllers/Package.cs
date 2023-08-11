namespace Common.Controllers;

public class Package<T>
{
    public T Result { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime CreatedDate { get; set; }

    protected internal Package(T result, string errorMessage)
    {
        Result = result;
        ErrorMessage = errorMessage;
        CreatedDate = DateTime.Now;
    }
}

public class Package : Package<string>
{
    protected Package(string errorMessage) : base(null, errorMessage) { }

    public static Package<T> Ok<T>(T result)
    {
        return new Package<T>(result, null);
    }

    public static Package Ok()
    {
        return new Package(null);
    }

    public static Package Error(string errorMessage)
    {
        return new Package(errorMessage);
    }
}