using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;

namespace Cephiro.Listings.Application.Reservation.Contracts.Response;


public sealed class CancelReservationResponse 
{
    public bool IsError { get; set; }
    public Error? Error { get; set; }
} 