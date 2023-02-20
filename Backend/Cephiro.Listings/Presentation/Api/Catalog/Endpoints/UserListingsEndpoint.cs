using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Catalog.Endpoints;
using ErrorOr;

namespace Cephiro.Listings.Presentation.Api.Catalog.Endpoints;

[Route("/userlistings")]
public sealed class UserListingsEndpoint : EndpointBaseAsync
    .WithRequest<UserListingsRequest>
    .WithActionResult<UserListingsResponse>
{
    private readonly IRequestClient<UserListingsRequest> _info;
    public UserListingsEndpoint(IRequestClient<UserListingsRequest> info)
    {
        _info = info;
    }

    [HttpPost]
    public override async Task<ActionResult<UserListingsResponse>> HandleAsync([FromBody] UserListingsRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _info.GetResponse<UserListingsResponse>(request);
        return response.Message;
    }
}

