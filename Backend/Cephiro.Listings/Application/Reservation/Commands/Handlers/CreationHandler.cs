using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class CreateReservationHandler: IConsumer<CreateReservationRequest>
{
    private readonly IReservationExecute _catalogRepository; 
    private readonly IValidator<CreateReservationRequest> _validator; 

    public CreateReservationHandler(IReservationExecute catalogRepository, IValidator<CreateReservationRequest> validator)
    {
        _validator = validator;
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<CreateReservationRequest> context)
    {
        CreateReservationResponse result = new () {IsError = false, Error = null};

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
            await context.RespondAsync<CreateReservationResponse>(result);
            return;
        }



        var response = await _catalogRepository.CreateReservation(context.Message, context.CancellationToken); 

        if(response.Error is null)
        {
            await context.RespondAsync<CreateReservationResponse>(result);
        }

        result.Error = response.Error;
        result.IsError = false;
        await context.RespondAsync<CreateReservationResponse>(result);
    }

}