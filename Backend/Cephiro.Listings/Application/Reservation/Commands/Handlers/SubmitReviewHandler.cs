using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class SubmitReviewHandler: IConsumer<SubmitReviewRequest>
{
    private readonly IReservationExecute _catalogRepository; 

    public SubmitReviewHandler(IReservationExecute catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<SubmitReviewRequest> context)
    {
        SubmitReviewResponse result = new () {IsError = false, Error = null};

        var response = await _catalogRepository.SubmitReview(context.Message, context.CancellationToken); 

        if(response.Error is null)
        {
            await context.RespondAsync<SubmitReviewResponse>(result);
        }

        result.Error = response.Error;
        result.IsError = false;
        await context.RespondAsync<SubmitReviewResponse>(result);
    }

}