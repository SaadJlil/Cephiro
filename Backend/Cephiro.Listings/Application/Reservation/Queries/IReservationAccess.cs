using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;

namespace Cephiro.Listings.Application.Reservation.Queries;


public interface IReservationAccess
{
    public Task<ListingReservationDatesResponse> GetListingReservations(ListingReservationDatesRequest Listing, CancellationToken token);
}