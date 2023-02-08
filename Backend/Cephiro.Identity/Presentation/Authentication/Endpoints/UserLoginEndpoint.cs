using Ardalis.ApiEndpoints;
using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Application.Authentication.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Authentication.Endpoints;

public sealed class UserLoginEndpoint : EndpointBaseAsync
    .WithRequest<UserLoginRequest>
    .WithActionResult<bool>

{
    private readonly IRequestClient<UserLoginRequest> _mediator;
    public UserLoginEndpoint(IRequestClient<UserLoginRequest> mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/login", Name = "login")]
    public override async Task<ActionResult<bool>> HandleAsync(UserLoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.GetResponse<UserLoggedInResponse>(request, cancellationToken);

        if (response.Message.Error!.Message is not null) return StatusCode(
            response.Message.Error!.Code, 
            response.Message.Error!.Message);

        Response.Cookies.Append("access_token", response.Message.Jwt!, new CookieOptions { HttpOnly = true });
        return Ok(true);
    }
}