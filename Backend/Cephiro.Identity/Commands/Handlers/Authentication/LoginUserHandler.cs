using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Contracts.Response;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.Enum;
using Cephiro.Identity.Queries.IAccessors;
using ErrorOr;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Authentication;

public sealed class LoginUserHandler : IConsumer<UserLoginRequest>
{
    private readonly IUserAuthExecutor _authExecute;
    private readonly IUserAuthAccessor _authAccess;
    private readonly JwtTokenGenerator _token;

    public LoginUserHandler(IUserAuthExecutor authExecute, IUserAuthAccessor authAccess, JwtTokenGenerator token)
    {
        _authExecute = authExecute;
        _authAccess = authAccess;
        _token = token;
    }

    public async Task Consume(ConsumeContext<UserLoginRequest> context)
    {
        var req = context.Message;

        ErrorOr<User> authResult = await _authAccess.GetUserByEmail(req.Email, context.CancellationToken);
            
        if(authResult.IsError) 
        {
            await context.RespondAsync(authResult.FirstError);
            await context.ConsumeCompleted;
        }

        if(!PasswordHashProvider.VerifyPassword(req.Password.Value, authResult.Value.PasswordHash!, authResult.Value.PasswordHash!))
        {
            await context.RespondAsync(Error.Validation("The password you entered is incorrect"));
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(new UserLoggedInResponse { Jwt = _token.GenerateToken(authResult.Value)});
        await _authExecute.UpdateUserStatus(authResult.Value.Id, Activity.ACTIVE, context.CancellationToken);
        await context.ConsumeCompleted;
    }
}