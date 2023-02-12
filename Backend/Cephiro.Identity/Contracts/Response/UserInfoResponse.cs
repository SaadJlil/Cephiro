using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Contracts.Response;

public sealed record UserInfoResponse
{
    [EmailAddress] public required string Email { get; init; }
    [Phone]        public string? Phone { get; init;}
    public required string FirstName { get; init;}
    public required string LastName { get; init;}
}