using Cephiro.Identity.Application.Shared.Contracts;

namespace Cephiro.Identity.Application.Reporting.Contracts.Response;

public sealed class MetricsResponse
{
    public bool? LockoutEnabled { get; set; }
    public int? UserCount { get; set; }
    public int? ActiveUserCount { get; set; }
    public int? UnverifiedUserCount { get; set; }
    public Error? Error { get; set; }
}