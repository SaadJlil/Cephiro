using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Metrics;
using Cephiro.Identity.Contracts.Response.Metrics;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Metrics.Endpoints;

public sealed class UnverifiedUserCountEndpoint: EndpointBaseAsync
    .WithRequest<UnverifiedUserCountRequest>
    .WithActionResult<ErrorOr<UnverifiedUserCountResponse>>

{
    private readonly IRequestClient<UnverifiedUserCountRequest> _request;
    public UnverifiedUserCountEndpoint(IRequestClient<UnverifiedUserCountRequest> request)
    {
        _request = request;
    }

    public override async Task<ActionResult<ErrorOr<UnverifiedUserCountResponse>>> HandleAsync(UnverifiedUserCountRequest input, CancellationToken cancellationToken = default)
    {
        var response = await _request.GetResponse<UnverifiedUserCountResponse>(cancellationToken);

        return Ok(response);
    }
}