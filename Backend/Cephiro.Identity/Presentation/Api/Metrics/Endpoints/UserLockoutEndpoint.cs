using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Metrics;
using Cephiro.Identity.Contracts.Response.Metrics;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Metrics.Endpoints;

public sealed class UserLockoutEndpoint : EndpointBaseAsync
    .WithRequest<UserLockoutRequest>
    .WithActionResult<ErrorOr<UserLockoutResponse>>

{
    private readonly IRequestClient<UserLockoutRequest> _request;
    public UserLockoutEndpoint(IRequestClient<UserLockoutRequest> request)
    {
        _request = request;
    }

    public override async Task<ActionResult<ErrorOr<UserLockoutResponse>>> HandleAsync(UserLockoutRequest input, CancellationToken cancellationToken = default)
    {
        var response = await _request.GetResponse<UserLockoutResponse>(cancellationToken);

        return Ok(response);
    }
}