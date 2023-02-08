using Cephiro.Identity.Application.Authentication.Commands.Helpers;
using Cephiro.Identity.Application.Authentication.Contracts.Request;
using Cephiro.Identity.Application.Authentication.Contracts.Response;
using Cephiro.Identity.Application.Authentication.Queries;
using Cephiro.Identity.Application.Authentication.Queries.Helpers;
using Cephiro.Identity.Application.Shared.Contracts;
using MassTransit;

namespace Cephiro.Identity.Application.Authentication.Commands.Handlers;

public sealed class ChangePasswordHandler : IConsumer<ChangePasswordRequest>
{
    private readonly IAuthAccess _authAccess;
    private readonly IAuthExecute _authExecute;
    public ChangePasswordHandler(IAuthAccess authAccess, IAuthExecute authExecute)
    {
        _authAccess = authAccess;
        _authExecute = authExecute;
    }
    public async Task Consume(ConsumeContext<ChangePasswordRequest> context)
    {
        Error error = new() {};
        var id = context.Message.GetIdFromJwtHeader();
        var oldPass = context.Message.OldPassword; var newPass = context.Message.NewPassword;

        var dbHash = await _authAccess.GetHashedPassword(id, context.CancellationToken);

        if(dbHash.Error is not null) 
        {
            await context.RespondAsync<PasswordUpdateResponse>(new { IsError = true, dbHash.Error});
            return;
        }

        var hash = dbHash.PasswordHash; var salt = dbHash.PasswordSalt;

        if(!PasswordHashValidator.VerifyPassword(oldPass, hash!, salt!))
        {
            error.Code = 400;
            error.Message = "The password is incorrect";

            await context.RespondAsync<PasswordUpdateResponse>(new { IsError = true, Error = error});
            return;
        }

        
        PasswordHashGenerator.GeneratePasswordHash(newPass, out byte[] newHash, out byte[] newSalt);

        var result = await _authExecute.ChangeUserPassword(id, newHash, newSalt, context.CancellationToken);
        if(result.Error is not null)
        {
            error = result.Error;

            await context.RespondAsync<PasswordUpdateResponse>(new { IsError = true, Error = error });
            return;
        }

        await context.RespondAsync<PasswordUpdateResponse>(new { IsError = false});
        return;

    }
}