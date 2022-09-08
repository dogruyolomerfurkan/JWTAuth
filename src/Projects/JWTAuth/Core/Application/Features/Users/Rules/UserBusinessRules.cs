using Application.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Hashing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Rules;

public class UserBusinessRules
{
    private readonly IReadRepository<User> _readRepository;

    public UserBusinessRules(IReadRepository<User> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task UserEmailCannotBeDuplicatedWhenInserted(string email)
    {
        var result = await _readRepository.AnyAsync(p => p.Email.ToLower() == email);
        if (result) throw new BusinessException("Email already exist");
    }

    public async Task UserEmailCannotBeDuplicatedWhenUpdated(int id, string email)
    {
        var result = await _readRepository.AnyAsync(p => p.Id != id && p.Email == email);
        if (result) throw new BusinessException("Email already exist");
    }

    public async Task<User> UserMustBeExistWhenGetById(int id)
    {
        var currentUser = await _readRepository.GetByIdAsync(id);

        if (currentUser is null) throw new BusinessException("User not exist");

        return currentUser;
    }

    public async Task<User> UserPasswordMustMatchWhenLogin(string email, string password)
    {
        var currentUser = await _readRepository.Get(p => p.Email == email, false).Include(p => p.UserRoles).ThenInclude(p => p.Role).FirstOrDefaultAsync();

        if (currentUser is null) throw new BusinessException("User not exist");

        var passwordMatchResult = HashingHelper.VerifyPasswordHash(password, currentUser.PasswordHash, currentUser.PasswordSalt);

        if (!passwordMatchResult) throw new BusinessException("User password is wrong");

        return currentUser;
    }
}