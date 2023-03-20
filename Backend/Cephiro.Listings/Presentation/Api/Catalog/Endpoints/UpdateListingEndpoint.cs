using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

[Route("/updatelisting")]
public sealed class UpdateListingEndpoint: EndpointBaseAsync
    .WithRequest<UpdateListingRequest>
    .WithActionResult<CreationResponse>
{
    private readonly IRequestClient<UpdateListingRequest> _update;
    public UpdateListingEndpoint(IRequestClient<UpdateListingRequest> update)
    {
        _update = update;
    }

    [HttpPost]
    public override async Task<ActionResult<CreationResponse>> HandleAsync([FromBody] UpdateListingRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _update.GetResponse<CreationResponse>(request);
        return response.Message;
    }
}

