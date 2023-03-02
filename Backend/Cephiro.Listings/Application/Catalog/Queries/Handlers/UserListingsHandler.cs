using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;
using ErrorOr;


namespace Cephiro.Listings.Application.Catalog.Queries.Handlers;


public sealed class UserListingsHandler: IConsumer<UserListingsRequest>
{
    private readonly ICatalogAccess _catalogRepository; 
    private readonly IValidator<UserListingsRequest> _validator;

    public UserListingsHandler(ICatalogAccess catalogRepository, IValidator<UserListingsRequest> validator)
    {
        _validator = validator;
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<UserListingsRequest> context)
    {

        //validation 
        var validation = await _validator.ValidateAsync(context.Message);
        if(!validation.IsValid)
        {
            await context.RespondAsync<UserListingsResponse>(new UserListingsResponse{
                IsError = true,
                Message = validation.Errors.
                                Select(x => x.ErrorMessage).
                                Aggregate((i, j) => i + " \n " + j)

            });
            return;
        }

        var response = await _catalogRepository.UserListings(context.Message, context.CancellationToken); 
        await context.RespondAsync<UserListingsResponse>(response ?? new UserListingsResponse{});
   }

}