using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class ListingReservationDatesRequest 
{
    public required Guid ListingId { get; set; }
    //public required DateTime StartDate { get; set; } ****Might want to add this for optimization purposes
    //public required DateTime EndDate { get; set; } 

}