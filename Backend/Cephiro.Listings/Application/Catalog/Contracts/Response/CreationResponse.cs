using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;

namespace Cephiro.Listings.Application.Catalog.Contracts.Response;


public sealed class CreationResponse
{
    public bool IsError { get; set; }
    public Error? Error { get; set; }
} 