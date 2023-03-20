using MassTransit;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using FluentValidation;
using ErrorOr;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;


namespace Cephiro.Listings.Application.Catalog.Queries.Handlers;


public sealed class GetListingInfoHandler: IConsumer<ListingInfoRequest>
{
    private readonly ICatalogAccess _catalogRepository; 
    private readonly IValidator<ListingInfoRequest> _validator; 

    public GetListingInfoHandler(ICatalogAccess catalogRepository, IValidator<ListingInfoRequest> validator)
    {
        _validator = validator;
        _catalogRepository = catalogRepository;
    }

    public async Task Consume(ConsumeContext<ListingInfoRequest> context)
    {

        //validation 
        var validation = await _validator.ValidateAsync(context.Message);
        if(!validation.IsValid)
        {
            await context.RespondAsync<ListingInfoResponse>(new ListingInfoResponse{
                Info = Error.Failure(validation.Errors.
                        Select(x => x.ErrorMessage).Aggregate((i, j) => i + "\n" + j)) 
            });
            return;
        }


        var response = await _catalogRepository.GetListingInfo(context.Message, context.CancellationToken); 
        
        await context.RespondAsync<ListingInfoResponse>(new ListingInfoResponse{
            Info = response
        });
        return;
    }
}