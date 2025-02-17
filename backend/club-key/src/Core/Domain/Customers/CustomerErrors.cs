namespace Domain.Abstractions;

public static class CustomerErrors
{
    public static Error NotFound = new(
        "Customer.NotFound",
        "El cliente no existe"
    );
    
    public static Error DuplicatedEntity = new(
        "Customer.DuplicatedEntity",
        "El cliente con el numero de documento proporcionado ya existe"
    );
}