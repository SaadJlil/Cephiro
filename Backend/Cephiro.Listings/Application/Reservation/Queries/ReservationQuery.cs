using Cephiro.Listings.Application.Reservation.Contracts.Request;
using MassTransit;

namespace Cephiro.Listings.Application.Reservation.Queries;

public static class ReservationQuery
{
    public static void AddReservationQueries(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<ListingReservationDatesRequest>();
    }
}