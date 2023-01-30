using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.Enum;
using Cephiro.Identity.Domain.ValueObjects;
using ErrorOr;

namespace Cephiro.Identity.Commands.IExecutors;

public interface IUserAuthExecutor
{
    public Task<ErrorOr<bool>> RegisterNewUser(User user, CancellationToken token);
    public Task<ErrorOr<bool>> ChangeUserPassword(Guid id, byte[] passwordHash, byte[] passwordSalt, CancellationToken token);
    public Task<ErrorOr<bool>> UpdateUserStatus(Guid id, Activity status, CancellationToken token);
}