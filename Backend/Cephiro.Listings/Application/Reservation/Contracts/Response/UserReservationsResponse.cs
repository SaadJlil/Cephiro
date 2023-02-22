using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;

namespace Cephiro.Listings.Application.Reservation.Contracts.Response;


public sealed class UserReservationsResponse
{
    public IEnumerable<UserReservationsInternal>? ListReservations { get; set; }
    public bool IsError { get; set; } = false;
    public string? Message { get; set; }
    public int? Code { get; set; }
} 