using Cephiro.Identity.Application.Authentication.Contracts.Internal;

namespace Cephiro.Identity.Application.Authentication.Queries;

public interface IAuthAccess
{
    public Task<UserEntityInternal> GetUserByEmail(string email, CancellationToken token);
    public Task<HashedPasswordInternal> GetHashedPassword(Guid id, CancellationToken token);
}