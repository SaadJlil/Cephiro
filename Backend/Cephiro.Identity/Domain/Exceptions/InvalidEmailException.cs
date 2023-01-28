namespace Cephiro.Identity.Domain.Exceptions;

public class InvalidEmailException : Exception
{
    public string EmailAddress { get; } = null!;

    public InvalidEmailException()
        : base("Invalid email address.")
    {
    }

    public InvalidEmailException(string email)
        : base($"The  email address : {email} is invalid")
    {
        EmailAddress = email;
    }

    public InvalidEmailException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}