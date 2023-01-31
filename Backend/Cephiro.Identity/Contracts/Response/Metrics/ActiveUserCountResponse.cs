using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Contracts.Response.Metrics;

public sealed record ActiveUserCountResponse 
{
    public required int ActiveUserCount{ get; set; }
}