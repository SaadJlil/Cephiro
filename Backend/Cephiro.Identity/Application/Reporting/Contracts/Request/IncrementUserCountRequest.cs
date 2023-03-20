namespace Cephiro.Identity.Application.Reporting.Contracts.Request;

public sealed record IncrementUserCountRequest
{
    public bool Increment { get; set; }
}