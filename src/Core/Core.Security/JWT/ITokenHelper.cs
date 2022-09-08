using Core.Security.Models;
using Domain.Entities;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken CreateToken(User user, IEnumerable<Role> userRoles);
}
