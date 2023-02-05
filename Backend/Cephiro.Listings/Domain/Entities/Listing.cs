using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cephiro.Listings.Domain.Entities;

[Table(name: "listing", Schema = "listings")]
public sealed class Listings
{
    [Column("id")] public Guid Id {get; set;}
    [Column("images")] public IEnumerable<Uri>? Imageuris { get; set; }//might want to change this. But since it has little to no performance issues, it doesn't seem like problem 
    [Column("location")] public Location Addresse { get; set; }
    [Column("description")] [MaxLength(500)] public string? Description { get; set; }

    [Column("number_views")] public int Views { get; set; }
//    [Column("reservation")] public Reservation Reservations { get; set; }
    [Column("price_day")] public float Price_day { get; set; }
    [Column("Number_reserved_days")] public int Number_reserved_days { get; set; }
    [Column("creation_date")] public DateTime Creation_date { get; set; }

    [Column("average_stars")] public float Average_stars { get; set; }
 //   [Column("user")] public Users User{ get; set; }
    [Column("Name")] [MaxLength(70)] public string? Name { get; set; }
}