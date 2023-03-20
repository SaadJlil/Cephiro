using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class SubmitReviewHandler: IConsumer<SubmitReviewRequest>
{
    private readonly IReservationExecute _catalogRepository; 
    private readonly IValidator<SubmitReviewRequest> _validator; 

    public SubmitReviewHandler(IReservationExecute catalogRepository, IValidator<SubmitReviewRequest> validator)
    {
        _validator = validator;
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<SubmitReviewRequest> context)
    {
        SubmitReviewResponse result = new () {IsError = false, Error = null};

        //validation 
        var validation = await _validator.ValidateAsync(context.Message);
        if(!validation.IsValid)
        {
            result.Error = new Error{ 
                Message = validation.Errors.
                                Select(x => x.ErrorMessage).
                                    Aggregate((i, j) => i + " \n " + j)
            };
            result.IsError = true;
            await context.RespondAsync<SubmitReviewResponse>(result);
            return;
        }




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