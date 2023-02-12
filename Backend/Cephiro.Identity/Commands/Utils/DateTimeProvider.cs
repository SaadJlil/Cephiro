namespace Cephiro.Identity.Commands.Utils;

public sealed class DateTimeProvider
{
    public DateTime Now { get; } = DateTime.UtcNow;
}