namespace Cephiro.Identity.Application.Shared.Contracts.Internal;

public struct DbWriteInternal
{
    public int? ChangeCount { get; set; }
    public Error? Error { get; set; }
}