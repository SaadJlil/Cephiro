namespace Cephiro.Identity.Application.Reporting.Contracts.Request;

public sealed class IncrementActiveUsersRequest
{
    public bool Increment { get; set; }
}