using Application.Features.Users.Rules;
using Application.Repositories;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserRequest, LoginUserResponse>
{
    private readonly IReadRepository<User> _readRepository;
    private readonly UserBusinessRules _userBusinessRules;
    private readonly ITokenHelper _tokenHelper;

    public LoginUserQueryHandler(IReadRepository<User> readRepository, UserBusinessRules userBusinessRules, ITokenHelper tokenHelper)
    {
        _readRepository = readRepository;
        _userBusinessRules = userBusinessRules;
        _tokenHelper = tokenHelper;
    }

    public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var currentUser = await _userBusinessRules.UserPasswordMustMatchWhenLogin(request.Email, request.Password);

        var accessToken = _tokenHelper.CreateToken(currentUser, currentUser.UserRoles.Select(p => p.Role).ToList());

        return new LoginUserResponse { AccessToken = accessToken };
    }
}
