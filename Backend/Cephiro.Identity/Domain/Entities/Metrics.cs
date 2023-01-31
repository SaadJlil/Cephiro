using System.ComponentModel.DataAnnotations.Schema;

namespace Cephiro.Identity.Domain.Entities;

[Table(name: "metrics", Schema = "identity")]
public sealed class Metrics
{
    [Column("id")] public int Id { get; set; }
    [Column("lockout_enabled")] public bool LockoutEnabled { get; set; }
    [Column("user_count")] public int UserCount { get; set; }
    [Column("active_user_count")] public int ActiveUserCount { get; set; }
    [Column("unverified_user_count")] public int UnverifiedUserCount { get; set; }
}