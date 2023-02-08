using Ardalis.ApiEndpoints;
using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Application.Authentication.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Authentication.Endpoints;

public sealed class UserRegistrationEndpoint : EndpointBaseAsync
    .WithRequest<UserRegistrationRequest>
    .WithActionResult<bool>
{
    private readonly IRequestClient<UserRegistrationRequest> _mediator;
    public UserRegistrationEndpoint(IRequestClient<UserRegistrationRequest> mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/register", Name = "Register")]
    public override async Task<ActionResult<bool>> HandleAsync(UserRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.GetResponse<UserLoggedInResponse>(request, cancellationToken);

        if(response.Message.Error is not null) return StatusCode(
            response.Message.Error.Code, 
            response.Message.Error.Message);
        
        Response.Cookies.Append("access_token", response.Message.Jwt!, new CookieOptions { HttpOnly = true, IsEssential = true });
        return Ok(true);
    }
}
