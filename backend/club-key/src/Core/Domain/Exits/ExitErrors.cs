namespace Domain.Abstractions;

public static class ExitErrors
{
    public static Error NotFound = new(
        "Entrance.Found",
        "La salida no existe"
    );
    
    public static Error DuplicatedEntity = new(
        "Entrance.DuplicatedEntity",
        "La salida con el identificador proporcionado ya existe"
    );
    
    public static Error MustBeHaveAnEntrance = new(
        "Entrance.MustBeHaveAnExit",
        "El cliente debe tener una entrada antes de registrar una nueva salida."
    );
    
    public static Error Exception(string exMessage) => new(
        "Entrance.Exception",
        $"Error al registrar la salida. {exMessage}"
    );
}