using FluentValidation;

namespace Application.Features.Users.Queries.LoginUser;

public class LoginUserQueryValidator : AbstractValidator<LoginUserRequest>
{
	public LoginUserQueryValidator()
	{
		RuleFor(p => p.Email).EmailAddress().NotEmpty();
		RuleFor(p => p.Password).Length(6, 100).NotEmpty();
	}
}
