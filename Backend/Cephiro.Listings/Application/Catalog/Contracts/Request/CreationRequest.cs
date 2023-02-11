using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class CreationRequest
{
    [Required] public required IEnumerable<Uri> Images { get; set; }
    [Required] public Location? Addresse { get; set; }
    [MaxLength(500)] public string? Description { get; set; }
    [Required] public float Price_day { get; set; }
    public IEnumerable<string>? Tags { get; set; }
 //   [Column("user")] public Users User{ get; set; }
    [MaxLength(70)] [Required] public string? Name { get; set; }
}