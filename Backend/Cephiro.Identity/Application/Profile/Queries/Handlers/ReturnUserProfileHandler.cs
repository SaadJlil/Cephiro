using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Profile.Contracts.Response;
using MassTransit;

namespace Cephiro.Identity.Application.Profile.Queries.Handlers;

public sealed class ReturnUserProfileHandler : IConsumer<UserProfileRequest>
{
    private readonly IProfileAccess _access;
    public ReturnUserProfileHandler(IProfileAccess access)
    {
        _access = access;
    }
    public async Task Consume(ConsumeContext<UserProfileRequest> context)
    {
        UserProfileResponse result = new() {};

        if(context.Message.userId == Guid.Empty)
        {
            result.Error = new() { Code = 400, Message = "Invalid operation. This user does not exist."};
            await context.RespondAsync<UserProfileResponse>(result);

            return;
        }

        result = await _access.GetUserProfile(context.Message.userId, context.CancellationToken);
        await context.RespondAsync<UserProfileResponse>(result);
        
        return;
    }
}