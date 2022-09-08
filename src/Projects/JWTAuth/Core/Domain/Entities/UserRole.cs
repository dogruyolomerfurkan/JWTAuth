using Domain.Entities.Common;

namespace Domain.Entities;

public class UserRole : BaseEntity
{
    public UserRole() { }
    public UserRole(int userId, int roleId) : this()
    {
        UserId = userId;
        RoleId = roleId;
    }

    public int UserId { get; set; }
    public User? User { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
}