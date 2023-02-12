using Cephiro.Identity.Domain.Exceptions;
using ValueOf;

namespace Cephiro.Identity.Domain.ValueObjects;

public sealed class Password : ValueOf<string, Password>
{
    protected override void Validate()
    {
        if(Value.Length < 8) throw new InvalidPasswordException("Password must be at least 8 characters long.");

        if (!Value.Any(char.IsDigit)) throw new InvalidPasswordException("Password must contain at least one number.");

        if (!Value.Any(char.IsUpper)) throw new InvalidPasswordException("Password must contain at least one capital letter.");

        if (!Value.Any(c => !char.IsLetterOrDigit(c))) throw new InvalidPasswordException("Password must contain at least one special character.");
    }
}