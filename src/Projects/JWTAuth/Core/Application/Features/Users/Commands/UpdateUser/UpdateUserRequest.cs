using MediatR;

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserRequest : IRequest<UpdateUserResponse>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
