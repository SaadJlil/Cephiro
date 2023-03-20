using Cephiro.Identity.Application.Shared.Contracts.Internal;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.Enum;

namespace Cephiro.Identity.Application.Authentication.Commands;

public interface IAuthExecute
{
    public Task<DbWriteInternal> RegisterNewUser(User user, CancellationToken token);
    public Task<DbWriteInternal> ChangeUserPassword(Guid id, byte[] passwordHash, byte[] passwordSalt, CancellationToken token);
    public Task<DbWriteInternal> UpdateUserStatus(Guid id, Activity status, CancellationToken token);
}