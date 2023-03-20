using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

[Route("/deletelisting")]
public sealed class DeleteListingEndpoint: EndpointBaseAsync
    .WithRequest<DeleteListingRequest>
    .WithActionResult<CreationResponse>
{
    private readonly IRequestClient<DeleteListingRequest> _delete;
    public DeleteListingEndpoint(IRequestClient<DeleteListingRequest> delete)
    {
        _delete = delete;
    }

    [HttpPost]
    public override async Task<ActionResult<CreationResponse>> HandleAsync([FromBody] DeleteListingRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _delete.GetResponse<CreationResponse>(request);
        return response.Message;
    }
}

