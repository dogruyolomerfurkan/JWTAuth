using Application.Features.Users.Rules;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserRequest, UpdateUserResponse>
{
    private readonly IWriteRepository<User> _writeRepository;
    private readonly UserBusinessRules _userBusinessRules;

    public UpdateUserCommandHandler(IWriteRepository<User> writeRepository, UserBusinessRules userBusinessRules)
    {
        _writeRepository = writeRepository;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await _userBusinessRules.UserEmailCannotBeDuplicatedWhenUpdated(request.Id, request.Email);

        var currentUser = await _userBusinessRules.UserMustBeExistWhenGetById(request.Id);

        _writeRepository.Update(currentUser);

        var updatedUser = request.Adapt(currentUser);

        await _writeRepository.SaveAsync();

        return updatedUser.Adapt<UpdateUserResponse>();
    }
}