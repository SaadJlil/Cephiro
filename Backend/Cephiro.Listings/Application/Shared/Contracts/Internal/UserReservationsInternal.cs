namespace Cephiro.Listings.Application.Shared.Contracts.Internal;


public sealed class UserReservationsInternal 
{
    //public required string ImageUri { get; set; }
    public required string Name { get; set; }
    public required float InvoiceValue { get; set; }
    public required Guid ReservationId { get; set; }
    public required Guid ListingId { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required DateTime ReservationDate { get; set; }
}
