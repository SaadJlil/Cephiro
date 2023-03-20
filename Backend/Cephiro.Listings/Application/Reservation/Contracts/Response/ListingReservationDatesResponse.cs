using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;

namespace Cephiro.Listings.Application.Reservation.Contracts.Response;


public sealed class ListingReservationDatesResponse
{
    public IEnumerable<ListingReservationDatesInternal>? ListDates { get; set; }
    public bool IsError { get; set; } = false;
    public string? Message { get; set; }
    public int? Code { get; set; }
} 