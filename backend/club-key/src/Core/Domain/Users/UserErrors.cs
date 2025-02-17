namespace Domain.Users;

using Abstractions;

public static class UserErrors
{
    public static Error NotFound = new(
        "User.Found",
        "No existe el usuario"
    );

    public static Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Las credenciales son incorrectas"
    );

    public static Error AlreadyExistsEmail = new(
        "User.AlreadyExistsEmail",
        "El email del usuario ya existe en la base de datos"
    );
    
    public static Error AlreadyExistsUsername = new(
        "User.AlreadyExistsUsername",
        "El username del usuario ya existe en la base de datos"
    );
    
    public static Error NotRegistered = new(
        "User.NotRegistered",
        "El usuario no ha sido registrado"
    );
    
    public static Error NotDeleted = new(
        "User.NotDeleted",
        "El usuario no ha sido eliminado"
    );
    
    public static Error NotUpdated = new(
        "User.NotUpdated",
        "No se pudo actualizar el usuario"
    );
    
    public static Error Inactive = new(
        "User.UserInactive",
        "El usuario esta bloqueado, contacte al admin"
    );
    
    public static Error ResetNewError = new(
        "User.ResetNewError",
        "Error al restablecer la contraseña"
    );
}