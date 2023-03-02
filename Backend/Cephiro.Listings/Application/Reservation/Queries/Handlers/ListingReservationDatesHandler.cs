using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Queries.Handlers;


public sealed class ListingReservationDatesHandler: IConsumer<ListingReservationDatesRequest>
{
    private readonly IReservationAccess _ReservationRepository; 
    private readonly IValidator<ListingReservationDatesRequest> _validator; 

    public ListingReservationDatesHandler(IReservationAccess ReservationRepository, IValidator<ListingReservationDatesRequest> validator)
    {
        _validator = validator;
        _ReservationRepository = ReservationRepository;
    }

    public async Task Consume(ConsumeContext<ListingReservationDatesRequest> context)
    {
        //validation 
        var validation = await _validator.ValidateAsync(context.Message);
        if(!validation.IsValid)
        {
            await context.RespondAsync<ListingReservationDatesResponse>(new ListingReservationDatesResponse{
                IsError = true,
                Message = validation.Errors.
                        Select(x => x.ErrorMessage).Aggregate((i, j) => i + "\n" + j),
            });
            return;
        }

        var response = await _ReservationRepository.GetListingReservationDates(context.Message, context.CancellationToken); 
        await context.RespondAsync<ListingReservationDatesResponse>(response);
   }

}