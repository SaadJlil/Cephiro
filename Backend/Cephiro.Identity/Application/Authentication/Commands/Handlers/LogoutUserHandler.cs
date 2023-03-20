using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Domain.Enum;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands.Handlers;

public sealed class LogoutUserHandler : IConsumer<UserLogoutRequest>
{
    private readonly IAuthExecute _userAuth;

    public LogoutUserHandler(IAuthExecute userAuth)
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
            await context.RespondAsync(false);
            return;
        }

        await context.RespondAsync(true);

        await _userAuth.UpdateUserStatus(id, Activity.DISCONNECTED, context.CancellationToken);

        return;
    }
}