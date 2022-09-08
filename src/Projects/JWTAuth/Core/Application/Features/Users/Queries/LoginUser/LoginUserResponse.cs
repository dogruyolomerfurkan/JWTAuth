using Core.Security.Models;

namespace Application.Features.Users.Queries.LoginUser;

public class LoginUserResponse
{
    public AccessToken AccessToken { get; set; }
}
