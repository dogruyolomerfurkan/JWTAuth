using Domain.Entities.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public User() { UserRoles = new List<UserRole>(); }

    public User(string firstName, string lastName, string email, byte[] passwordSalt, byte[] passwordHash) : this()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordSalt = passwordSalt;
        PasswordHash = passwordHash;
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
}