using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class DeleteUserListings: IConsumer<DeleteUserListingsRequest>
{
    private readonly ICatalogExecute _catalogRepository; 
    private readonly IValidator<DeleteUserListingsRequest> _validator; 

    public DeleteUserListings(ICatalogExecute catalogRepository, IValidator<DeleteUserListingsRequest> validator)
    {
        _validator = validator;
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<DeleteUserListingsRequest> context)
    {
        CreationResponse result = new () {IsError = false, Error = null};

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
            await context.RespondAsync<CreationResponse>(result);
            return;
        }


        var response = await _catalogRepository.DeleteUserListings(context.Message, context.CancellationToken); 

        if(response.Error is null)
        {
            await context.RespondAsync<CreationResponse>(result);
            return;
        }

        result.Error = response.Error;
        result.IsError = true;
        await context.RespondAsync<CreationResponse>(result);
        return;
    }

}