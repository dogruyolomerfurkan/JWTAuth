using FluentValidation;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserCommandValidator()
    {
        RuleFor(p => p.FirstName).Length(2, 50).NotEmpty();
        RuleFor(p => p.LastName).Length(2, 50).NotEmpty();
        RuleFor(p => p.Email).EmailAddress().MaximumLength(100).NotEmpty();
        RuleFor(p => p.Password).NotEmpty().Length(6, 50);
    }
}
