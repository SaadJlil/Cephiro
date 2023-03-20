using Cephiro.Identity.Application.Profile.Contracts.Request;
using Cephiro.Identity.Application.Profile.Contracts.Response;
using MassTransit;

namespace Cephiro.Identity.Application.Profile.Queries.Handlers;

public sealed class ReturnUserListHandler : IConsumer<UserListRequest>
{
    private readonly IProfileAccess _access;
    public ReturnUserListHandler(IProfileAccess access)
    {
        _access = access;
    }
    public async Task Consume(ConsumeContext<UserListRequest> context)
    {
        var request = context.Message;
        UsersListResponse result = new() {};

        if(request.Take is not 20 or 50 or 100)
        {
            result.Error = new() { Code = 400, Message = "Invalid Operation." };
            await context.RespondAsync<UsersListResponse>(result);

            return;
        }

        result = await _access.GetUsers(request.Take, request.Skip, context.CancellationToken);
        await context.RespondAsync<UsersListResponse>(result);

        return;
    }
}
