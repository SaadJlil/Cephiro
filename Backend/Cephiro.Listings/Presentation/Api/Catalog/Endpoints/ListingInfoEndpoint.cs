using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;
using ErrorOr;

namespace Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

[Route("/getlistinginfo")]
public sealed class ListingInfoEndpoint : EndpointBaseAsync
    .WithRequest<ListingInfoRequest>
    .WithActionResult<ListingInfoResponse>
{
    private readonly IRequestClient<ListingInfoRequest> _info;
    public ListingInfoEndpoint(IRequestClient<ListingInfoRequest> info)
    {
        _info = info;
    }

    [HttpPost]
    public override async Task<ActionResult<ListingInfoResponse>> HandleAsync([FromBody] ListingInfoRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _info.GetResponse<ListingInfoResponse>(request);
        return response.Message;
    }
}

