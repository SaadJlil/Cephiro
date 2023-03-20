using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;

namespace Cephiro.Listings.Application.Reservation.Queries;


public interface IReservationAccess
{
    public Task<ListingReservationDatesResponse> GetListingReservationDates(ListingReservationDatesRequest Listing, CancellationToken token);
    public Task<UserReservationsResponse> GetUserReservations(UserReservationsRequest reservation, CancellationToken token);
}