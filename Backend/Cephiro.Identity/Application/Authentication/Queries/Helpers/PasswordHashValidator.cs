using System.Security.Cryptography;

namespace Cephiro.Identity.Application.Authentication.Queries.Helpers;

public static class PasswordHashValidator
{
    public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var hmac = new HMACSHA256(passwordSalt);
        return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)) == passwordHash;
    }
}
