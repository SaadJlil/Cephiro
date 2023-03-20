using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Search.Contracts.Request;
using Cephiro.Listings.Application.Search.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Search.Endpoints;
using ErrorOr;

namespace Cephiro.Listings.Presentation.Api.Search.Endpoints;

[Route("/listingsearch")]
public sealed class ListingSearchEndpoint: EndpointBaseAsync
    .WithRequest<ListingSearchRequest>
    .WithActionResult<ListingSearchResponse>
{
    private readonly IRequestClient<ListingSearchRequest> _listings;
    public ListingSearchEndpoint(IRequestClient<ListingSearchRequest> listings)
    {
        _listings = listings;
    }

    [HttpPost]
    public override async Task<ActionResult<ListingSearchResponse>> HandleAsync([FromBody] ListingSearchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _listings.GetResponse<ListingSearchResponse>(request);
        return response.Message;
    }
}

