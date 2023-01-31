using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Contracts.Response.Metrics;

public sealed record UserLockoutResponse 
{
    public required bool LockoutEnabled{ get; set; }
}