namespace Cephiro.Identity.Contracts.Request.Reporting;

public sealed record DecrementUnverifiedUsersRequest
{
    public bool Decrement { get; set; }
}