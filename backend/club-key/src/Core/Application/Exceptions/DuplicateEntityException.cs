namespace Application.Exceptions;

public class DuplicateEntityException : ApplicationException
{
    public DuplicateEntityException(string message) : base(message) { }
    
    public DuplicateEntityException(string entityName, string key) 
        : base($"La entidad '{entityName}' con el identificador '{key}' ya existe.") { }
}