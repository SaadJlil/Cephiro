using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Application.Shared.CustomAttributes;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class CreateReservationRequest 
{
    public required Guid UserId { get; set; }
    public required Guid ListingId { get; set; }
    [StartTimeLimiter] public required DateTime StartDate { get; set; } 
    [EndTimeLimiter] public required DateTime EndDate { get; set; } 

}