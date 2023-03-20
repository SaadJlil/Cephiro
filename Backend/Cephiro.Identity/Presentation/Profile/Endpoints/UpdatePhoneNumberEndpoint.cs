using Ardalis.ApiEndpoints;
using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Shared.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Profile.Endpoints;

public class UpdatePhoneNumberEndpoint : EndpointBaseAsync
.WithRequest<UpdatePhoneNumberRequest>
.WithActionResult<bool>
{
    private readonly IRequestClient<UpdatePhoneNumberRequest> _mediator;
    public UpdatePhoneNumberEndpoint(IRequestClient<UpdatePhoneNumberRequest> mediator)
    {
        _mediator = mediator;
    }
    public async override Task<ActionResult<bool>> HandleAsync(UpdatePhoneNumberRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.GetResponse<UpdateRecordResponse>(request, cancellationToken);

        if(result.Message.Error is not null) return StatusCode(result.Message.Error.Code, result.Message.Error.Message);
        else return Ok(result.Message.Updated);
    }
}
