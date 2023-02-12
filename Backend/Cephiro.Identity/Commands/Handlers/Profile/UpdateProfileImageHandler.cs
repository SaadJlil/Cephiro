using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Contracts.Request.Profile;
using ErrorOr;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Profile;

public sealed class UpdateProfileImageHandler : IConsumer<UpdateProfileImageRequest>
{
    private readonly IUserProfileExecutor _userProfile;
    public UpdateProfileImageHandler(IUserProfileExecutor userProfile)
    {
        _userProfile = userProfile;
    }
    public async Task Consume(ConsumeContext<UpdateProfileImageRequest> context)
    {
        Guid id = context.Message.GetIdFromRequestHeaders();

        if(id == Guid.Empty)
        {
            await context.RespondAsync(Error.Validation("Please, verify that you are currently logged in"));
            await context.ConsumeCompleted;
        }

        var result = await _userProfile.UpdateUserImage(id, context.Message.ImageUri, context.CancellationToken);
        
        if(result.IsError)
        {
            await context.RespondAsync(result.FirstError);
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(result.Value);
        await context.ConsumeCompleted;
    }
}