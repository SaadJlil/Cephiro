using Cephiro.Identity.Commands.IExecutors;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class UserProfileExecutor : IUserProfileExecutor
{
    public Task ChangeUserPassword()
    {
        throw new NotImplementedException();
    }

    public Task UpdateDescription()
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserImage()
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserPhoneNumber()
    {
        throw new NotImplementedException();
    }
}