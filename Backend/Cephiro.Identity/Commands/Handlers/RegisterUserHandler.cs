using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Contracts.Request;
using Cephiro.Identity.Domain.Entities;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers;

public sealed class RegisterUserCommand : IConsumer<UserRegistrationRequest>
{
    private readonly IUserAuthExecutor _authRepository;
    private readonly JwtTokenGenerator _token;

    public RegisterUserCommand(IUserAuthExecutor authRepository, JwtTokenGenerator token, CancellationToken cancellationToken)
    {
        _authRepository = authRepository;
        _token = token;
    }
    
    public async Task Consume(ConsumeContext<UserRegistrationRequest> context)
    {
        
        PasswordHashProvider.GeneratePasswordHash(context.Message.Password.Value, out var hash, out var salt);

        if(hash is null || salt is null)
            throw new NotImplementedException();

        User user = RegistrationMapper.Map(context.Message, hash, salt);

        if(!await _authRepository.RegisterNewUser(user, context.CancellationToken))
            throw new NotImplementedException();
        
        string jwt = _token.GenerateToken(user);
        
        await context.RespondAsync(jwt);
    }

}