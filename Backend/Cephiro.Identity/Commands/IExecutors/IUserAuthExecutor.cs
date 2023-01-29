using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.ValueObjects;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserAuthExecutor
{
    public Task<bool> RegisterNewUser(User user, CancellationToken cancellation);
    public Task<User?> SignUserIn(string email, Password password, CancellationToken cancellation);
    public Task<bool> SignUserOut(Guid id, CancellationToken cancellation);
}