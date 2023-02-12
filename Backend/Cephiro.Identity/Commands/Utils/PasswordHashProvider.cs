using System.Security.Cryptography;

namespace Cephiro.Identity.Commands.Utils;

public static class PasswordHashProvider
{
    public static void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA256())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        };
    }

    public static bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        var hmac = new HMACSHA256(passwordSalt);
        return hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)) == passwordHash;
    }
}