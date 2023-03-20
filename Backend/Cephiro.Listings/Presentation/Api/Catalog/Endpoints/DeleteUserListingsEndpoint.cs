using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

[Route("/deleteuserlisting")]
public sealed class DeleteUserListingsEndpoint: EndpointBaseAsync
    .WithRequest<DeleteUserListingsRequest>
    .WithActionResult<CreationResponse>
{
    private readonly IRequestClient<DeleteUserListingsRequest> _deleteuser;
    public DeleteUserListingsEndpoint(IRequestClient<DeleteUserListingsRequest> deleteuser)
    {
        _deleteuser = deleteuser;
    }

    [HttpPost]
    public override async Task<ActionResult<CreationResponse>> HandleAsync([FromBody] DeleteUserListingsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _deleteuser.GetResponse<CreationResponse>(request);
        return response.Message;
    }
}

