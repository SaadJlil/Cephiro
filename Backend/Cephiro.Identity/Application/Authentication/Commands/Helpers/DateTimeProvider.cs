namespace Cephiro.Identity.Application.Authentication.Commands.Helpers;

public sealed class DateTimeProvider
{
    public DateTime Now { get; } = DateTime.UtcNow;
}