using FluentValidation;

namespace Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleRequest>
{
	public CreateRoleCommandValidator()
	{
		RuleFor(p => p.Name).Length(2, 50).NotEmpty();
	}
}