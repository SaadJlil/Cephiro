using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Contracts.Response;
using Cephiro.Identity.Domain.Entities;
using ErrorOr;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Authentication;

public sealed class RegisterUserCommand : IConsumer<UserRegistrationRequest>
{
    private readonly IUserAuthExecutor _authRepository;
    private readonly JwtTokenGenerator _token;

    public RegisterUserCommand(IUserAuthExecutor authRepository, JwtTokenGenerator token)
    {
        _authRepository = authRepository;
        _token = token;
    }
    
    public async Task Consume(ConsumeContext<UserRegistrationRequest> context)
    {

        PasswordHashProvider.GeneratePasswordHash(context.Message.Password.Value, out var hash, out var salt);

        User user = RegistrationMapper.Map(context.Message, hash, salt);
        
        var result = await _authRepository.RegisterNewUser(user, context.CancellationToken);

        if(result.IsError)
        {
            await context.RespondAsync(result.FirstError);
            await context.ConsumeCompleted;
        }


        await context.RespondAsync(new UserLoggedInResponse { Jwt = _token.GenerateToken(user)});
        await context.ConsumeCompleted;
    }

}