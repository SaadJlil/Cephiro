using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Metrics;
using Cephiro.Identity.Contracts.Response.Metrics;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Metrics.Endpoints;

public sealed class ActiveUserCountEndpoint : EndpointBaseAsync
    .WithRequest<ActiveUserCountRequest>
    .WithActionResult<ErrorOr<ActiveUserCountResponse>>

{
    private readonly IRequestClient<ActiveUserCountRequest> _request;
    public ActiveUserCountEndpoint(IRequestClient<ActiveUserCountRequest> request)
    {
        _request = request;
    }

    public override async Task<ActionResult<ErrorOr<ActiveUserCountResponse>>> HandleAsync(ActiveUserCountRequest input, CancellationToken cancellationToken = default)
    {
        var response = await _request.GetResponse<ActiveUserCountResponse>(cancellationToken);

        return Ok(response);
    }
}