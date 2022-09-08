using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserRequest>
{
	public UpdateUserCommandValidator()
	{
        RuleFor(p => p.FirstName).Length(2, 50).NotEmpty();
        RuleFor(p => p.LastName).Length(2, 50).NotEmpty();
        RuleFor(p => p.Email).EmailAddress().MaximumLength(100).NotEmpty();
    }
}
