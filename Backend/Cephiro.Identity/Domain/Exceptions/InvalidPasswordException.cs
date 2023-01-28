namespace Cephiro.Identity.Domain.Exceptions;

public sealed class InvalidPasswordException : Exception
{
    public InvalidPasswordException(string message)
        : base(message)
    {
    }

    public InvalidPasswordException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}