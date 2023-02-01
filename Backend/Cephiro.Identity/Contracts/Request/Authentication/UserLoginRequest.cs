using System.ComponentModel.DataAnnotations;
using Cephiro.Identity.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Contracts.Request.Authentication;


public sealed record UserLoginRequest
{
    [FromBody] [MaxLength(256)] [EmailAddress] public required string Email { get; set; }
    [FromBody] [MaxLength(40)]                 public required Password Password { get; set; }
}