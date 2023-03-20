namespace Cephiro.Identity.Application.Shared.Contracts;
public sealed class UpdateRecordResponse
{
    public bool Updated { get; set; }
    public Error? Error { get; set; }
}
