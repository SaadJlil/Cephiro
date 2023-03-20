using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Contracts.Response;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Authentication.Endpoints;

public sealed class UserLoginEndpoint : EndpointBaseAsync
    .WithRequest<UserLoginRequest>
    .WithActionResult<ErrorOr<string>>
{
    private readonly IRequestClient<UserLoginRequest> _login;
    public UserLoginEndpoint(IRequestClient<UserLoginRequest> login)
    {
        _login = login;
    }

    public override async Task<ActionResult<ErrorOr<string>>> HandleAsync(UserLoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _login.GetResponse<UserLoggedInResponse>(new {});

        if (response.Message.Jwt.IsError)
            return NotFound(response.Message.Jwt.FirstError);

        else 
        {
            Response.Cookies.Append("access_token", response.Message.Jwt.Value, new CookieOptions { HttpOnly = true });
            return Ok(true);
        }
    }
}