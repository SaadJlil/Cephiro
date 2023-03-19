using MassTransit;
using Cephiro.Listings.Application.Search.Contracts.Response;
using Cephiro.Listings.Application.Search.Contracts.Request;
//using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Search.Queries;
using Cephiro.Listings.Application.Shared.Contracts;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using FluentValidation;


namespace Cephiro.Listings.Application.Search.Queries.Handlers;


public sealed class ListingSearchHandler: IConsumer<ListingSearchRequest>
{
    private readonly ISearchAccess _SearchRepository; 
    private readonly IValidator<ListingSearchRequest> _validator; 

    public ListingSearchHandler(ISearchAccess SearchRepository, IValidator<ListingSearchRequest> validator)
    {
        _validator = validator;
        _SearchRepository = SearchRepository;
    }

    public async Task Consume(ConsumeContext<ListingSearchRequest> context)
    {

        //validation 
        var validation = await _validator.ValidateAsync(context.Message);

        if(!validation.IsValid)
        {
            await context.RespondAsync<ListingSearchResponse>(new ListingSearchResponse{
                IsError = true,
                Message = validation.Errors.
                        Select(x => x.ErrorMessage).Aggregate((i, j) => i + "\n" + j),
            });
            return;
        }

        var response = await _SearchRepository.GetListingSearch(context.Message, context.CancellationToken); 
        await context.RespondAsync<ListingSearchResponse>(response ?? new ListingSearchResponse{});

   }

}