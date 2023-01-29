using System.ComponentModel.DataAnnotations;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.ValueObjects;

namespace Cephiro.Identity.Contracts.Request;

public sealed record UserRegistrationRequest
{
    [MaxLength(256)] [EmailAddress] public required string Email { get; set; }
    [MaxLength(256)]                public required string FirstName { get; set; }
    [MaxLength(128)]                public string? MiddleName { get; set; }
    [MaxLength(128)]                public required string LastName { get; set; }
    [MaxLength(40)]                 public required Password Password { get; set; }
}

public static class RegistrationMapper
{
    public static User Map(UserRegistrationRequest request, byte[] passwordHash, byte[] passwordSalt)
    {
        return new User 
        {
            Email = request.Email,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
    }
}