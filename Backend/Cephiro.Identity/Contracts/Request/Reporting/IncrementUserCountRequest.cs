namespace Cephiro.Identity.Contracts.Request.Reporting;

public sealed record IncrementUserCountRequest
{
    public bool Increment { get; set; }
}