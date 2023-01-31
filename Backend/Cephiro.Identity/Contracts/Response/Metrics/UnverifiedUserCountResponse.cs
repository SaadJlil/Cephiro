using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Contracts.Response.Metrics;

public sealed record UnverifiedUserCountResponse 
{
    public required int UnverifiedUserCount{ get; set; }
}