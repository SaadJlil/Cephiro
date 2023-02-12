using ErrorOr;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserProfileExecutor
{
    public Task<ErrorOr<bool>> UpdateUserPhoneNumber(Guid id, string phonenumber, CancellationToken token);
    public Task<ErrorOr<bool>> UpdateUserImage(Guid id, Uri imageUri, CancellationToken token);
    public Task<ErrorOr<bool>> UpdateDescription(Guid id, string description, CancellationToken token);
}