using System.ComponentModel.DataAnnotations;

namespace Cephiro.Listings.Domain.Entities;

public class Photos: BaseEntity<Guid>
{
    [Required] public Listings Listing { get; set; }
    [Required] public Uri? Image { get; set; }
}