using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Reservation.Contracts.Request;

namespace Cephiro.Listings.Application.Reservation.Commands;


public interface IReservationExecute 
{
    public Task<DbWriteInternal> CreateReservation(CreateReservationRequest reservation, CancellationToken token);
    public Task<DbWriteInternal> CancelReservation(CancelReservationRequest reservation, CancellationToken token);
}