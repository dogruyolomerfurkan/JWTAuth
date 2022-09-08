using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;

namespace Application.Features.Roles.Rules;

public class RoleBusinessRules
{
    private readonly IReadRepository<Role> _readRepository;

    public RoleBusinessRules(IReadRepository<Role> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task RoleCannotBeDuplicatedWhenInserted(string role)
    {
        var result = await _readRepository.AnyAsync(p => p.Name == role);
        if (result) throw new BusinessException("Role already exist");
    }
}