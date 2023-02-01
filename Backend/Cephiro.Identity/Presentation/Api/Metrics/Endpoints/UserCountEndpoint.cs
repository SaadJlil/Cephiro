using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Metrics;
using Cephiro.Identity.Contracts.Response.Metrics;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Metrics.Endpoints;

public sealed class UserCountEndpoint : EndpointBaseAsync
    .WithRequest<UserCountRequest>
    .WithActionResult<ErrorOr<UserCountResponse>>

{
    private readonly IRequestClient<UserCountRequest> _request;
    public UserCountEndpoint(IRequestClient<UserCountRequest> request)
    {
        _request = request;
    }

    public override async Task<ActionResult<ErrorOr<UserCountResponse>>> HandleAsync(UserCountRequest input, CancellationToken cancellationToken = default)
    {
        var response = await _request.GetResponse<UserCountResponse>(cancellationToken);

        return Ok(response);
    }
}