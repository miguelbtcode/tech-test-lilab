namespace Application.Features.Auth.Users.Queries.GetUserById;

using FluentValidation;

public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("{PropertyName} is required.");
    }
}