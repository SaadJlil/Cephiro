namespace Cephiro.Identity.Domain.Entities;

public sealed class Configuration
{
    public int Id { get; set; }
    public bool LockoutEnabled { get; set; }
    public int UserCount { get; set; }
}