using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Contracts.Request;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers;

public sealed class LogoutUserHandler : IConsumer<UserLogoutRequest>
{
    private readonly IUserAuthExecutor _userAuth;
    public LogoutUserHandler(IUserAuthExecutor userAuth)
    {
        _userAuth = userAuth;
    }
    public async Task Consume(ConsumeContext<UserLogoutRequest> context)
    {
        var id = context.Message.Id;

        await _userAuth.SignUserOut(id, context.CancellationToken);

        await context.RespondAsync(true);
    }
}