using System.ComponentModel.DataAnnotations;

namespace Cephiro.Listings.Domain.Entities;

public class Photos: BaseEntity<Guid>
{
    public required Listings Listing { get; set; }
    public required Uri? Image { get; set; }
}