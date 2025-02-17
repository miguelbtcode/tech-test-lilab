namespace Application.Features.Customers.Commands.RegisterCustomer;

using FluentValidation;

public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.");
        
        RuleFor(p => p.DocumentNumber)
            .NotEmpty().WithMessage("Document number is required.")
            .Matches("^[0-9]{8}$").WithMessage("Document number must be exactly 8 digits.");
        
        RuleFor(p => p.Type)
            .NotNull().WithMessage("Customer type is required.")
            .IsInEnum().WithMessage("Invalid customer type.");
        
        RuleFor(p => p.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{9}$").WithMessage("Phone number must have exactly 9 numeric digits.");
        
        RuleFor(p => p.BirthDate)
            .NotNull().WithMessage("BirthDate is required.")
            .Must(birthDate => birthDate.HasValue && birthDate.Value <= DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("BirthDate must be a valid date and cannot be in the future.");
        
        RuleFor(p => p.Gender)
            .NotNull().WithMessage("Gender is required.")
            .IsInEnum().WithMessage("Gender must be a valid value.");
        
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
