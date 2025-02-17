namespace Application.Features.Auth.Users.Commands.LoginUser;

using FluentValidation;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
        
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}