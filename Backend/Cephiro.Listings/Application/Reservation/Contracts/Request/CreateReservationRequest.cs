using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class CreateReservationRequest 
{
    public required Guid UserId { get; set; }
    public required Guid ListingId { get; set; }
    public required DateTime ReservationDate { get; set; } = DateTime.Now.ToUniversalTime();
    public required DateTime StartDate { get; set; } 
    public required DateTime EndDate { get; set; } 

}