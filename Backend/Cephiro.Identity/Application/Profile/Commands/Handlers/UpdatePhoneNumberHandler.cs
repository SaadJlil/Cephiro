using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Shared.Contracts;
using Cephiro.Identity.Application.Shared.Helpers;
using MassTransit;

namespace Cephiro.Identity.Application.Profile.Commands.Handlers;

public sealed class UpdatePhoneNumberHandler : IConsumer<UpdatePhoneNumberRequest>
{
    private readonly IProfileExecute _userProfile;

    public UpdatePhoneNumberHandler(IProfileExecute userProfile)
    {
        _userProfile = userProfile;
    }

    public async Task Consume(ConsumeContext<UpdatePhoneNumberRequest> context)
    {
        UpdateRecordResponse response = new() { Updated = false, Error = null };

        Guid id = context.Message.GetIdFromRequestBody();

        if(id == Guid.Empty)
        {
            response.Error = new() { Code = 400, Message = "You need to be authenticated first" };
            await context.RespondAsync<UpdateRecordResponse>(response);
            return;
        }

        var validNumber = PhoneNumberUtils.ValidNumber(context.Message.NewPhoneNumber);

        if(validNumber.Error is not null)
        {
            response.Error = validNumber.Error;
            await context.RespondAsync(response);
            return;
        }

        PhoneNumberUtils.ConvertToInternationalNumber(validNumber.PhoneNumber!, out var international);
        

        var result = await _userProfile.UpdateUserPhoneNumber(id, international, context.CancellationToken);
        
        if(result.Error is not null)
        {
            response.Error = result.Error;
            await context.RespondAsync(response);
            return;
        }

        response.Updated = true; response.Error = null;

        await context.RespondAsync(response);
        return;
    }
}