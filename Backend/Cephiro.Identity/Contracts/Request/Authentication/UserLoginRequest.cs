using System.ComponentModel.DataAnnotations;
using Cephiro.Identity.Domain.ValueObjects;

namespace Cephiro.Identity.Contracts.Request.Authentication;

public sealed record UserLoginRequest
{
    [MaxLength(256)] [EmailAddress] public required string Email { get; set; }
    [MaxLength(40)]                 public required Password Password { get; set; }
}