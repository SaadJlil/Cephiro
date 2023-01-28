using Cephiro.Identity.Domain.ValueObjects;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserProfileExecutor
{
    public Task<bool> ChangeUserPassword(Guid id, Password oldPass, Password newPass);
    public Task<bool> UpdateUserPhoneNumber(Guid id, string phonenumber);
    public Task<bool> UpdateUserImage(Guid id, Uri imageUri);
    public Task<bool> UpdateDescription(Guid id, string description);
}