using MediatR;

namespace Application.Features.Roles.Commands.CreateRole;

public class CreateRoleRequest : IRequest<CreateRoleResponse>
{
    public string Name { get; set; }
}