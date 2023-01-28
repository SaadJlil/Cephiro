using System.Text.RegularExpressions;
using Cephiro.Identity.Domain.Exceptions;
using ValueOf;

namespace Cephiro.Identity.Domain.ValueObjects;

public sealed partial class Email : ValueOf<string, Password>
{
    private static readonly Regex EmailRegex = MyRegex();
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new InvalidEmailException(Value, new Exception("A value has to be inputed"));

        Value = Value.Trim();

        if (Value.Length > 254)
            throw new InvalidEmailException(Value);

        int atIndex = Value.IndexOf('@');
        if (atIndex < 1 || atIndex > 64)
            throw new InvalidEmailException(Value);

        int domainPartLength = Value.Length - atIndex - 1;
        if (domainPartLength < 1 || domainPartLength > 255)
            throw new InvalidEmailException(Value);

        if (!EmailRegex.IsMatch(Value))
            throw new InvalidEmailException(Value);
    }

    [GeneratedRegex("(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|\"(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21\\x23-\\x5b\\x5d-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])*\")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\\x01-\\x08\\x0b\\x0c\\x0e-\\x1f\\x21-\\x5a\\x53-\\x7f]|\\\\[\\x01-\\x09\\x0b\\x0c\\x0e-\\x7f])+)\\])", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}