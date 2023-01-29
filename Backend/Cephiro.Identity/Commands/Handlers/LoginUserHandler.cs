using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Contracts.Request;
using Cephiro.Identity.Domain.Entities;
using Cephiro.Identity.Domain.ValueObjects;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers;

public sealed class LoginUserHandler : IConsumer<UserLoginRequest>
{
    private readonly IUserAuthExecutor _userAuth;
    private readonly JwtTokenGenerator _token;
    public LoginUserHandler(IUserAuthExecutor userAuth, JwtTokenGenerator token)
    {
        _userAuth = userAuth;
        _token = token;
    }
    public async Task Consume(ConsumeContext<UserLoginRequest> context)
    {
        var email = context.Message.Email;
        var password = context.Message.Password;
        var cancellation = context.CancellationToken;

        User result = await _userAuth.SignUserIn (email, password, cancellation);
            
        if(result is null)
            throw new NotImplementedException();

        string jwt = _token.GenerateToken(result);
        
        await context.RespondAsync(jwt);
    }
}