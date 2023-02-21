using Cephiro.Listings.Application.Reservation.Contracts.Request;
using MassTransit;

namespace Cephiro.Listings.Application.Reservation.Commands;

public static class ReservationCommands
{
    public static void AddReservationCommands(this IMediatorRegistrationConfigurator cfg)
    {
        cfg.AddRequestClient<CreateReservationRequest>();
        cfg.AddRequestClient<CancelReservationRequest>();
    }
}