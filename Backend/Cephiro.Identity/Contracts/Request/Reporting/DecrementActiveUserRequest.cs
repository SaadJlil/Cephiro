namespace Cephiro.Identity.Contracts.Request.Reporting;

public sealed record DecrementActiveUserRequest
{
    public bool Decrement { get; set; }
}