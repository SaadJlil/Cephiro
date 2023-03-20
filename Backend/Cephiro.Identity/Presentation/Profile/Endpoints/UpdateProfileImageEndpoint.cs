using Ardalis.ApiEndpoints;
using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Shared.Contracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Cephiro.Identity.Presentation.Profile.Endpoints;

public class UpdateProfileImageEndpoint : 
EndpointBaseAsync
.WithRequest<UpdateProfileImageRequest>
.WithActionResult<bool>
{
    private readonly IRequestClient<UpdateProfileImageRequest> _mediator;
    public UpdateProfileImageEndpoint(IRequestClient<UpdateProfileImageRequest> mediator)
    {
        _mediator = mediator;
    }

    [HttpPatch("/UpdateProfileImage", Name = "Update Profile Image")]
    public async override Task<ActionResult<bool>> HandleAsync(UpdateProfileImageRequest request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.GetResponse<UpdateRecordResponse>(request, cancellationToken);
        
        if(result.Message.Error is not null) return StatusCode(result.Message.Error.Code, result.Message.Error.Message);
        else return Ok(result.Message.Updated);
    }
}
