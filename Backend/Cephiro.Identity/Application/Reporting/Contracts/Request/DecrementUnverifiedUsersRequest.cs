namespace Cephiro.Identity.Application.Reporting.Contracts.Request;

public sealed record DecrementUnverifiedUsersRequest
{
    public bool Decrement { get; set; }
}