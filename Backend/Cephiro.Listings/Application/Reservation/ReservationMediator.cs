using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Reservation.Queries;
using MassTransit;


namespace Cephiro.Listings.Application.Catalog;

public static class ReservationMediator 
{
    public static void AddReservationMediator(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddReservationCommands();
        cfg.AddReservationQueries();
    }
}