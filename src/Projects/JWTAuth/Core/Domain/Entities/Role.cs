using Domain.Entities.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public Role() { }

    public Role(string name) : this()
    {
        Name = name;
    }
    public string Name { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}