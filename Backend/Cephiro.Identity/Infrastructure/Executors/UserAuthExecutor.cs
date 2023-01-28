using Cephiro.Identity.Commands.IExecutors;

namespace Cephiro.Identity.Infrastructure.Executors;

public sealed class UserAuthExecutor : IUserAuthExecutor
{
    public Task RegisterNewUser()
    {
        throw new NotImplementedException();
    }

    public Task SignUserIn()
    {
        throw new NotImplementedException();
    }

    public Task SignUserOut()
    {
        throw new NotImplementedException();
    }
}