namespace Domain.Roles;

using Abstractions;

public static class RoleErrors
{
    public static Error NotFound = new(
        "Role.NotFound",
        "El rol no existe"
    );
    
    public static Error EmptyList = new(
        "Role.EmptyList",
        "No se encontraron roles disponibles"
    );
    
    public static Error NotUpdated = new(
        "Role.NotUpdated",
        "Error al asignar el nuevo rol"
    );
}