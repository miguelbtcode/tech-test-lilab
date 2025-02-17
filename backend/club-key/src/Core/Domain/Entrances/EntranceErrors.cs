namespace Domain.Abstractions;

public static class EntranceErrors
{
    public static Error NotFound = new(
        "Entrance.Found",
        "La entrada no existe"
    );
    
    public static Error NotExistsEntrance = new(
        "Entrance.NotExistsEntrance",
        "No existen entradas activas para el cliente"
    );
    
    public static Error DuplicatedEntity = new(
        "Entrance.DuplicatedEntity",
        "La entrada con el identificador proporcionado ya existe"
    );
    
    public static Error MustBeHaveAnExit = new(
        "Entrance.MustBeHaveAnExit",
        "El cliente debe tener una salida antes de registrar una nueva entrada."
    );
    
    public static Error Exception(string exMessage) => new(
        "Entrance.Exception",
        $"Error al registrar la entrada. {exMessage}"
    );
}