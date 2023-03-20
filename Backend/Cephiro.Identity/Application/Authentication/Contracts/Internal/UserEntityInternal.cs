using Cephiro.Identity.Application.Shared.Contracts;
using Cephiro.Identity.Domain.Entities;

namespace Cephiro.Identity.Application.Authentication.Contracts.Internal;

public struct UserEntityInternal
{
    public User? User { get; set; }
    public Error? Error { get; set; }
}