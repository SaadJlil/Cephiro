using Ardalis.ApiEndpoints;
using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Shared.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Profile.Endpoints;

public class UpdateProfileBioEndpoint : EndpointBaseAsync
.WithRequest<UpdateProfileBioRequest>
.WithActionResult<bool>
{
    private readonly IRequestClient<UpdateProfileBioRequest> _mediator;
    public UpdateProfileBioEndpoint(IRequestClient<UpdateProfileBioRequest> mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("/UpdateProfileBio", Name = "Update Profile Bio")]
    public async override Task<ActionResult<bool>> HandleAsync(UpdateProfileBioRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.GetResponse<UpdateRecordResponse>(request, cancellationToken);

        if(result.Message.Error is not null) return StatusCode(result.Message.Error.Code, result.Message.Error.Message);
        else return Ok(result.Message.Updated);
    }
}
