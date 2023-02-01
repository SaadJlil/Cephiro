using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Metrics;
using Cephiro.Identity.Contracts.Response.Metrics;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DomainEntities = Cephiro.Identity.Domain.Entities;


namespace Cephiro.Identity.Presentation.Api.Metrics.Endpoints;

public sealed class UserMetricsEndpoint : EndpointBaseAsync
    .WithRequest<UserMetricsRequest>
    .WithActionResult<ErrorOr<DomainEntities.Metrics>>

{
    private readonly IRequestClient<MetricsRequest> _request;
    public UserMetricsEndpoint(IRequestClient<MetricsRequest> request)
    {
        _request = request;
    }

    public override async Task<ActionResult<ErrorOr<DomainEntities.Metrics>>> HandleAsync(UserMetricsRequest input, CancellationToken cancellationToken = default)
    {
        var response = await _request.GetResponse<DomainEntities.Metrics>(cancellationToken);

        return Ok(response);
    }
}