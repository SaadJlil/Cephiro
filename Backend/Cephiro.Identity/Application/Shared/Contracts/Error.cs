namespace Cephiro.Identity.Application.Shared.Contracts;

public sealed class Error
{
    public int Code { get; set; }
    public string? Message { get; set; }
}