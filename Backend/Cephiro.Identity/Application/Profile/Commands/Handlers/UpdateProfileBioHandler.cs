using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Shared.Contracts;
using MassTransit;

namespace Cephiro.Identity.Application.Profile.Commands.Handlers;

public sealed class UpdateProfileBioHandler : IConsumer<UpdateProfileBioRequest>
{
    private readonly IProfileExecute _userProfile;
    public UpdateProfileBioHandler(IProfileExecute userProfile)
    {
        _userProfile = userProfile;
    }
    public async Task Consume(ConsumeContext<UpdateProfileBioRequest> context)
    {
        Guid id = context.Message.GetIdFromRequestHeaders();
        UpdateRecordResponse response = new() { Updated = false, Error = null};

        if(id == Guid.Empty)
        {
            response.Error = new() { Code = 400, Message = "You need to be authenticated first" };
            await context.RespondAsync<UpdateRecordResponse>(response);
            return;
        }

        var result = await _userProfile.UpdateDescription(id, context.Message.Description, context.CancellationToken);
        
        if(result.Error is not null)
        {
            response.Error = result.Error;
            await context.RespondAsync<UpdateRecordResponse>(response);
            return;
        }

        response.Updated = true; response.Error = null;
        await context.RespondAsync(response);
        
        return;
    }
}