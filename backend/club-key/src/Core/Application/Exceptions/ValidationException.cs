namespace Application.Exceptions;

using FluentValidation.Results;

public class ValidationException : ApplicationException
{
    public ValidationException() : base("One or more validations errors occurred") 
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(f => f.PropertyName, f => f.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}