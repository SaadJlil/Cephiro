using Cephiro.Identity.Application.Authentication.Commands.Helpers;
using Cephiro.Identity.Application.Authentication.Contracts.Internal;
using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Application.Authentication.Contracts.Response;
using Cephiro.Identity.Application.Authentication.Queries;
using Cephiro.Identity.Application.Authentication.Queries.Helpers;
using Cephiro.Identity.Application.Shared.Contracts;
using Cephiro.Identity.Domain.Enum;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands.Handlers;

public sealed class LoginUserCommand : IConsumer<UserLoginRequest>
{
    private readonly IAuthExecute _authExecute;
    private readonly IAuthAccess _authAccess;
    private readonly JwtTokenGenerator _token;

    public LoginUserCommand(IAuthExecute authExecute, IAuthAccess authAccess, JwtTokenGenerator token)
    {
        _authExecute = authExecute;
        _authAccess = authAccess;
        _token = token;
    }

    public async Task Consume(ConsumeContext<UserLoginRequest> context)
    {
        var req = context.Message;

        Error error = new() { Code = 400, Message = ""};
        UserLoggedInResponse result = new() { IsError = true, Error = error, Jwt = null};

        UserEntityInternal authResult = await _authAccess.GetUserByEmail(req.Email, context.CancellationToken);

        if(authResult.Error is not null) 
        {
            result.Error = authResult.Error;
            
            await context.RespondAsync<UserLoggedInResponse>(result);
            return;
        }

        if(!PasswordHashValidator.VerifyPassword(req.Password, authResult.User!.PasswordHash!, authResult.User.PasswordHash!))
        {
            error.Message = "The password you entered is incorrect";

            await context.RespondAsync<UserLoggedInResponse>(result);
            return;
        }

        result.Jwt = _token.GenerateToken(authResult.User);
        result.IsError = false;
        result.Error = null;

        await context.RespondAsync<UserLoggedInResponse>(result);
        await _authExecute.UpdateUserStatus(authResult.User.Id, Activity.ACTIVE, context.CancellationToken);
        return;
    }
}