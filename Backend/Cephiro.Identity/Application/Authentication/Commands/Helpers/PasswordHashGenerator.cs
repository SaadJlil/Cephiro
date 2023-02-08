using System.Security.Cryptography;

namespace Cephiro.Identity.Application.Authentication.Commands.Helpers;

public static class PasswordHashGenerator
{
    public static void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA256())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        };
    }
}