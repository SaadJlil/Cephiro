using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

[Route("/createlisting")]
public sealed class CreateListingEndpoint : EndpointBaseAsync
    .WithRequest<CreationRequest>
    .WithActionResult<CreationResponse>
{
    private readonly IRequestClient<CreationRequest> _create;
    public CreateListingEndpoint(IRequestClient<CreationRequest> create)
    {
        _create = create;
    }

    [HttpPost]
    public override async Task<ActionResult<CreationResponse>> HandleAsync([FromBody] CreationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _create.GetResponse<CreationResponse>(request);
        return response.Message;
    }
}

