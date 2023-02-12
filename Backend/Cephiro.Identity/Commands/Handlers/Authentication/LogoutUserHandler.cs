using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Domain.Enum;
using ErrorOr;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Authentication;

public sealed class LogoutUserHandler : IConsumer<UserLogoutRequest>
{
    private readonly IUserAuthExecutor _userAuth;

    public LogoutUserHandler(IUserAuthExecutor userAuth)
    {
        _userAuth = userAuth;
    }

    public async Task Consume(ConsumeContext<UserLogoutRequest> context)
    {
        var id = context.Message.GetIdFromJwtHeader();

        // the following LoC should be updated to notify the db of a fatal exception
        // a logout request should always contain the id of its user
        // if the id is empty, it may indicate that the api is accessed from an unsupported client 
        if(id == Guid.Empty) 
        {
            await context.RespondAsync(Error.Conflict());
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(true);

        await _userAuth.UpdateUserStatus(id, Activity.DISCONNECTED, context.CancellationToken);

        await context.ConsumeCompleted;
    }
}