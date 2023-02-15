using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cephiro.Listings.Domain.Entities;

[Table(name: "image")]
public class Photos: BaseEntity<Guid>
{
    public required Listings Listing { get; set; }
    public required Uri? Image { get; set; }
}