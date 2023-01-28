namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserProfileExecutor
{
    public Task ChangeUserPassword();
    public Task UpdateUserPhoneNumber();
    public Task UpdateUserImage();
    public Task UpdateDescription();
}