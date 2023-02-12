using Cephiro.Identity.Contracts.Response;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.ValueObjects;
using ErrorOr;

namespace Cephiro.Identity.Queries.IAccessors;

public interface IUserAuthAccessor
{
    public Task<ErrorOr<User>> GetUserByEmail(string email, CancellationToken token);
    public Task<ErrorOr<HashedPasswordResponse>> GetHashedPassword(Guid id, Password password, CancellationToken token);
}