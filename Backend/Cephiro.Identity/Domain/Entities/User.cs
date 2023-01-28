using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    [MaxLength(256)] public required string FirstName { get; set; }
    [MaxLength(128)] public string? MiddleName { get; set; }
    [MaxLength(128)] public required string LastName { get; set; }
    [MaxLength(256)] public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    [MaxLength(16)] public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
}