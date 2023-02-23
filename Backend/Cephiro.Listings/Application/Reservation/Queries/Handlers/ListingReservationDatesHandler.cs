using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Reservation.Queries.Handlers;


public sealed class ListingReservationDatesHandler: IConsumer<ListingReservationDatesRequest>
{
    private readonly IReservationAccess _ReservationRepository; 

    public ListingReservationDatesHandler(IReservationAccess ReservationRepository)
    {
        _ReservationRepository = ReservationRepository;
    }

    public async Task Consume(ConsumeContext<ListingReservationDatesRequest> context)
    {
        var response = await _ReservationRepository.GetListingReservationDates(context.Message, context.CancellationToken); 
        await context.RespondAsync<ListingReservationDatesResponse>(response);
   }

}