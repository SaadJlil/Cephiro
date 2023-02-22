using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class UserReservationsRequest 
{
    public required Guid UserId { get; set; }
}