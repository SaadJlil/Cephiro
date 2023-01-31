using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Contracts.Response.Metrics;

public sealed record UserCountResponse 
{
    public required int UserCount { get; set; }
}