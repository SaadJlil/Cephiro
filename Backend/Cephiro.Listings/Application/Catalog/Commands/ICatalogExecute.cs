using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Catalog.Contracts.Request;

namespace Cephiro.Listings.Application.Catalog.Commands;


public interface ICatalogExecute
{
    public Task<DbWriteInternal> CreateListing(CreationRequest listing, CancellationToken token);
    public Task<DbWriteInternal> UpdateListing(UpdateListingRequest Uplisting, CancellationToken token);
}