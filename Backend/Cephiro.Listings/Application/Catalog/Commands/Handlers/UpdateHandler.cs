using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Commands;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Catalog.Commands.Handlers;


public sealed class UpdateListingHandler: IConsumer<UpdateListingRequest>
{
    private readonly ICatalogExecute _catalogRepository; 
    private readonly IValidator<UpdateListingRequest> _validator; 

    public UpdateListingHandler(ICatalogExecute catalogRepository, IValidator<UpdateListingRequest> validator)
    {
        _validator = validator;
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<UpdateListingRequest> context)
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

        var response = await _catalogRepository.UpdateListing(context.Message, context.CancellationToken); 

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