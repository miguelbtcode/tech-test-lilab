namespace Domain.Abstractions;

public class Error
{
    public string Code { get; }
    public string Name { get; }

    public Error(string code, string name)
    {
        Code = code;
        Name = name;
    }

    public static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error NullValue = new("Error.NullValue", "Un valor Null fue ingresado");
}