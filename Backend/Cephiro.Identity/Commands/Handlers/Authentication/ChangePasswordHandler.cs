using Cephiro.Identity.Commands.IExecutors;
using Cephiro.Identity.Commands.Utils;
using Cephiro.Identity.Contracts.Request.Authentication;
using Cephiro.Identity.Queries.IAccessors;
using ErrorOr;
using MassTransit;

namespace Cephiro.Identity.Commands.Handlers.Authentication;

public sealed class ChangePasswordHandler : IConsumer<ChangePasswordRequest>
{
    private readonly IUserAuthAccessor _authAccess;
    private readonly IUserAuthExecutor _authExecute;
    public ChangePasswordHandler(IUserAuthAccessor authAccess, IUserAuthExecutor authExecute)
    {
        _authAccess = authAccess;
        _authExecute = authExecute;
    }
    public async Task Consume(ConsumeContext<ChangePasswordRequest> context)
    {
        var id = context.Message.GetIdFromJwtHeader();
        var oldPass = context.Message.OldPassword;
        var newPass = context.Message.NewPassword;

        var hashedValues = await _authAccess
            .GetHashedPassword(id, oldPass, context.CancellationToken);

        if(hashedValues.IsError) 
        {
            await context.RespondAsync(hashedValues.FirstError);
            await context.ConsumeCompleted;
        }

        var hash = hashedValues.Value.PasswordHash; var salt = hashedValues.Value.PasswordSalt;

        if(!PasswordHashProvider.VerifyPassword(context.Message.OldPassword.Value, hash, salt))
        {
            await context.RespondAsync(Error.Validation("The password is incorrect"));
            await context.ConsumeCompleted;
        }

        
        PasswordHashProvider.GeneratePasswordHash(newPass.Value, out byte[] newHash, out byte[] newSalt);

        var result = await _authExecute.ChangeUserPassword(id, newHash, newSalt, context.CancellationToken);
        if(result.IsError)
        {
            await context.RespondAsync(result.FirstError);
            await context.ConsumeCompleted;
        }

        await context.RespondAsync(true);
        await context.ConsumeCompleted;

    }
}