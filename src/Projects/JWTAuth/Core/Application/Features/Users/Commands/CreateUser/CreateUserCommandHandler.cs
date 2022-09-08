using Application.Features.Users.Rules;
using Application.Repositories;
using Core.Security.Hashing;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserRequest, CreateUserResponse>
{
    private readonly IWriteRepository<User> _writeRepository;
    private readonly UserBusinessRules _userBusinessRules;

    public CreateUserCommandHandler(IWriteRepository<User> writeRepository, UserBusinessRules userBusinessRules)
    {
        _writeRepository = writeRepository;
        _userBusinessRules = userBusinessRules;
    }

    public async Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        await _userBusinessRules.UserEmailCannotBeDuplicatedWhenInserted(request.Email);

        HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var newUser = request.Adapt<User>();
        newUser.PasswordHash = passwordHash;
        newUser.PasswordSalt = passwordSalt;

        await _writeRepository.AddAsync(newUser);

        await _writeRepository.SaveAsync();

        var createUserResponse = newUser.Adapt<CreateUserResponse>();
        createUserResponse.Password = request.Password;

        return createUserResponse;
    }
}