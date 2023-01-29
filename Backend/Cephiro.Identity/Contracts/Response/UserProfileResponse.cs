using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Contracts.Response;

public sealed record UserProfileResponse
{
    [MaxLength(400)]                public Uri? ImageUri { get; set; }
    [MaxLength(800)]                public string? Description { get; set; }
    [MaxLength(256)]                public required string FirstName { get; set; }
    [MaxLength(128)]                public string? MiddleName { get; set; }
    [MaxLength(128)]                public required string LastName { get; set; }
    [MaxLength(256)] [EmailAddress] public required string Email { get; set; }
    [MaxLength(16)] [Phone]         public string? PhoneNumber { get; set; }
}