namespace Cephiro.Identity.Application.Reporting.Contracts.Request;

public sealed record DecrementActiveUserRequest
{
    public bool Decrement { get; set; }
}