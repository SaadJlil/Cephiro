using Cephiro.Identity.Application.Authentication.Commands.Helpers;
using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Application.Authentication.Contracts.Response;
using Cephiro.Identity.Application.Shared.Contracts;
using Cephiro.Identity.Domain.Entities;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands.Handlers;

public sealed class RegisterUserHandler : IConsumer<UserRegistrationRequest>
{
    private readonly IAuthExecute _authRepository;
    private readonly JwtTokenGenerator _token;

    public RegisterUserHandler(IAuthExecute authRepository, JwtTokenGenerator token)
    {
        _authRepository = authRepository;
        _token = token;
    }
    
    public async Task Consume(ConsumeContext<UserRegistrationRequest> context)
    {
        Error error = new() { Code = 400, Message = ""};
        UserLoggedInResponse result = new() { IsError = true, Error = error, Jwt = null};

        PasswordHashGenerator.GeneratePasswordHash(context.Message.Password, out var hash, out var salt);

        User user = RegistrationMapper.Map(context.Message, hash, salt);
        
        var response = await _authRepository.RegisterNewUser(user, context.CancellationToken);

        if(response.Error is not null)
        {
            result.Error = response.Error;
            await context.RespondAsync<UserLoggedInResponse>(result);
            return;
        }

        result.IsError = false;
        result.Error = null;
        result.Jwt = _token.GenerateToken(user);

        await context.RespondAsync<UserLoggedInResponse>(result);
        return;
    }

}