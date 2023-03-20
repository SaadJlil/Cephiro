using Ardalis.ApiEndpoints;
using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Application.Authentication.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Authentication.Endpoints;

public class PasswordChangeEndpoint : EndpointBaseAsync
.WithRequest<ChangePasswordRequest>
.WithActionResult<bool>
{
    private readonly IRequestClient<ChangePasswordRequest> _mediator;
    public PasswordChangeEndpoint(IRequestClient<ChangePasswordRequest> mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("/UpdatePassword", Name = "Change Password")]
    public async override Task<ActionResult<bool>> HandleAsync(ChangePasswordRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.GetResponse<PasswordUpdateResponse>(request, cancellationToken);

        if(result.Message.IsError) return StatusCode(result.Message.Error!.Code, result.Message.Error.Message);

        else return Ok(!result.Message.IsError);
    }
}
