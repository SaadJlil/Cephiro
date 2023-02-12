using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Entities;

namespace Cephiro.Listings.Domain.Entities;

[Table(name: "listing")]
public sealed class Listings: BaseEntity<Guid>
{
    [Column("images")] [Required] public IEnumerable<Photos>? Images { get; set; }//might want to change this. But since it has little to no performance issues, it doesn't seem like problem 
    [Column("location")] [Required] public Location? Addresse { get; set; }
    [Column("description")] [MaxLength(500)] public string? Description { get; set; }

    [Column("number_views")] public int Views { get; set; } = 0;
//    [Column("reservation")] public Reservation Reservations { get; set; }
    [Column("price_day")] [Required] public float Price_day { get; set; }
    [Column("number_reserved_days")] public int Number_reserved_days { get; set; } = 0;
    [Column("creation_date")] [Required] public DateTime Creation_date { get; set; }
    [Column("creation_date")] [Required] public DateTime Creation_date { get; set; }
    [Column("average_stars")] public float Average_stars { get; set; } = 0;
    [Column("user")] public Users User{ get; set; }
    [Column("Name")] [MaxLength(70)] [Required] public string? Name { get; set; }
}


  "ConnectionStrings": {
    "ListingsConnection": "Server=localhost;Database=cephiro_listings;Port=5432;User Id=cephiro_user;Password=0000;"
  }

