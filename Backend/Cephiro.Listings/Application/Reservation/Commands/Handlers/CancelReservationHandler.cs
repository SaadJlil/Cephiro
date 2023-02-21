using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class CancelReservationHandler: IConsumer<CancelReservationRequest>
{
    private readonly IReservationExecute _catalogRepository; 

    public CancelReservationHandler(IReservationExecute catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<CancelReservationRequest> context)
    {
        CancelReservationResponse result = new () {IsError = false, Error = null};


        var response = await _catalogRepository.CancelReservation(context.Message, context.CancellationToken); 

        if(response.Error is null)
        {
            await context.RespondAsync<CancelReservationResponse>(result);
        }

        result.Error = response.Error;
        result.IsError = false;
        await context.RespondAsync<CreateReservationResponse>(result);
    }

}