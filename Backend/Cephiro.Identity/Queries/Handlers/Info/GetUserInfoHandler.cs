using Cephiro.Identity.Queries.IAccessor;
using Cephiro.Identity.Contracts.Request.Info; 
using MassTransit;
using ErrorOr;

namespace Cephiro.Identity.Queries.Handlers.Info;


public sealed class GetUserInfoHandler: IConsumer<UserInfoRequest>
{
    private readonly IUserAccess _useraccess;

    public GetUserInfoHandler(IUserAccess useraccess)
    {
        _useraccess = useraccess;
    }

    public async Task Consume(ConsumeContext<UserInfoRequest> context)
    {
        Guid id = context.Message.GetIdFromRequestHeaders();

        if(id == Guid.Empty)
        {
            await context.RespondAsync(Error.Validation("A wrong Id has been received"));
            await context.ConsumeCompleted;
        }

        var result = await _useraccess.GetUserInfoById(id, context.CancellationToken);

        if(result.IsError)
        {
            await context.RespondAsync(result.FirstError);
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(result.Value);
    }
}

