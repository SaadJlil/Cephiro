using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.ValueObjects;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserAuthExecutor
{
    public Task<bool> RegisterNewUser(User user);
    public Task<bool> SignUserIn(string email, Password password);
    public Task<bool> SignUserOut(Guid id);
}