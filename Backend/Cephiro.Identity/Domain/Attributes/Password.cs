using System.ComponentModel.DataAnnotations;

namespace Cephiro.Identity.Domain.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class Password : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if(value is not string) return false;

        return MatchesPassword((string)value);
    }

    internal static bool MatchesPassword(string value)
    {
        if (value.Length < 8) return false;
        if (!value.Any(char.IsDigit)) return false;
        if (!value.Any(char.IsUpper)) return false;
        // if (!value.Any(c => !char.IsLetterOrDigit(c))) return false;

        return true;
    }


}

