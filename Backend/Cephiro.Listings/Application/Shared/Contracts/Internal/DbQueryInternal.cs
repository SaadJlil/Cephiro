using Cephiro.Listings.Application.Shared.Contracts;

namespace Cephiro.Listings.Application.Shared.Contracts.Internal;

public struct DbQueryInternal
{
    public int? ChangeCount { get; set; }
    public Error? Error { get; set; }
}