using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cephiro.Listings.Domain.Entities;

[Table(name: "image")]
public class Photos: BaseEntity<Guid>
{
    [Column("listing")] public required Listings Listing { get; set; }
    [Column("imageuri")] public required Uri? ImageUri { get; set; }
}