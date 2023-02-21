using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class CancelReservationRequest
{
    public required Guid UserId { get; set; }
    public required Guid ReservationId { get; set; }

}