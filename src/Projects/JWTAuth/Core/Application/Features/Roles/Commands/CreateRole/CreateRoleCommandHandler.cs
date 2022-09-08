using Application.Features.Roles.Rules;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleRequest, CreateRoleResponse>
{
    private readonly IWriteRepository<Role> _writeRepository;
    private readonly RoleBusinessRules _roleBusinessRules;

    public CreateRoleCommandHandler(IWriteRepository<Role> writeRepository, RoleBusinessRules roleBusinessRules)
    {
        _writeRepository = writeRepository;
        _roleBusinessRules = roleBusinessRules;
    }

    public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        await _roleBusinessRules.RoleCannotBeDuplicatedWhenInserted(request.Name);

        var newRole = request.Adapt<Role>();
        await _writeRepository.AddAsync(newRole);

        await _writeRepository.SaveAsync();

        return newRole.Adapt<CreateRoleResponse>();
    }
}