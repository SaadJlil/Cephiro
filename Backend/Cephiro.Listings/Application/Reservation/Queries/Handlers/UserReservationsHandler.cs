using MassTransit;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Reservation.Queries.Handlers;


public sealed class UserReservationsHandler: IConsumer<UserReservationsRequest>
{
    private readonly IReservationAccess _ReservationRepository; 
    private readonly IValidator<UserReservationsRequest> _validator; 

    public UserReservationsHandler(IReservationAccess ReservationRepository, IValidator<UserReservationsRequest> validator)
    {
        _validator = validator;
        _ReservationRepository = ReservationRepository;
    }

    public async Task Consume(ConsumeContext<UserReservationsRequest> context)
    {
        //validation 
        var validation = await _validator.ValidateAsync(context.Message);
        if(!validation.IsValid)
        {
            await context.RespondAsync<UserReservationsResponse>(new UserReservationsResponse{
                IsError = true,
                Message = validation.Errors.
                        Select(x => x.ErrorMessage).Aggregate((i, j) => i + "\n" + j),
            });
            return;
        }

        var response = await _ReservationRepository.GetUserReservations(context.Message, context.CancellationToken); 
        await context.RespondAsync<UserReservationsResponse>(response);
   }

}