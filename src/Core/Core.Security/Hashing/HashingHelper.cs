using System.Security.Cryptography;
using System.Text;

namespace Core.Security.Hashing;

public class HashingHelper
{
    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwrodSalt)
    {
        using var hmac = new HMACSHA512();
        passwrodSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwrodSalt)
    {
        using var hmac = new HMACSHA512(passwrodSalt);
        var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        return !computedPasswordHash.Where((t, i) => t != passwordHash[i]).Any();
    }
}