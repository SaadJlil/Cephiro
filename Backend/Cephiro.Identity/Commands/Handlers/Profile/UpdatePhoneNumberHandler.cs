using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Contracts.Request.Profile;
using ErrorOr;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Profile;

public sealed class UpdatePhoneNumberHandler : IConsumer<UpdatePhoneNumberRequest>
{
    private readonly IUserProfileExecutor _userProfile;

    public UpdatePhoneNumberHandler(IUserProfileExecutor userProfile)
    {
        _userProfile = userProfile;
    }

    public async Task Consume(ConsumeContext<UpdatePhoneNumberRequest> context)
    {
        Guid id = context.Message.GetIdFromRequestBody();

        if(id == Guid.Empty)
        {
            await context.RespondAsync(Error.Validation("You need to authenticate to access this feature"));
            await context.ConsumeCompleted;
        }

        var validNumber = PhoneNumberUtils.IsValidNumber(context.Message.NewPhoneNumber);

        if(validNumber.IsError)
        {
            await context.RespondAsync(validNumber.FirstError);
            await context.ConsumeCompleted;
        }

        PhoneNumberUtils.ConvertToInternationalNumber(validNumber.Value, out var formattedIntNbr);
        

        var result = await _userProfile.UpdateUserPhoneNumber(id, formattedIntNbr, context.CancellationToken);
        
        if(result.IsError)
        {
            await context.RespondAsync(result.FirstError);
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(result.Value);
    }
}