using Cephiro.Identity.Application.Shared.Contracts.Internal;

namespace Cephiro.Identity.Application.Profile.Commands;

public interface IProfileExecute
{
    public Task<DbWriteInternal> UpdateUserPhoneNumber(Guid id, string phonenumber, CancellationToken token);
    public Task<DbWriteInternal> UpdateUserImage(Guid id, Uri imageUri, CancellationToken token);
    public Task<DbWriteInternal> UpdateDescription(Guid id, string description, CancellationToken token);
}