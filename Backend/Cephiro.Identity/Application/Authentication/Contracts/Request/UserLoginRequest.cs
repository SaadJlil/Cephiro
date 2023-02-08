using System.ComponentModel.DataAnnotations;
using Cephiro.Identity.Domain.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Application.Authentication.Contracts.Request;


public sealed record UserLoginRequest
{

    [FromBody] [Required] [MinLength(6)] [MaxLength(256)] [EmailAddress] 
    public required string Email { get; set; }


    [FromBody] [Required] [MinLength(8)] [MaxLength(40)] [Password]      
    public required string Password { get; set; }

}