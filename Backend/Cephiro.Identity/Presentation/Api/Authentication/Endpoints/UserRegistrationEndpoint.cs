using Ardalis.ApiEndpoints;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Contracts.Response;
using ErrorOr;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Api.Authentication.Endpoints;

public sealed class UserRegistrationEndpoint : EndpointBaseAsync
    .WithRequest<UserRegistrationRequest>
    .WithActionResult<ErrorOr<string>>
{
    private readonly IRequestClient<UserRegistrationRequest> _register;
    public UserRegistrationEndpoint(IRequestClient<UserRegistrationRequest> register)
    {
        _register = register;
    }
    public override async Task<ActionResult<ErrorOr<string>>> HandleAsync(UserRegistrationRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _register.GetResponse<UserLoggedInResponse>(cancellationToken);
        throw new NotImplementedException();
    }
}
