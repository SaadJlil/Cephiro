using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cephiro.Listings.Domain.Entities;

[Table(name: "reservation")]
public sealed class Reservations: BaseEntity<Guid>
{
    [Column("userid")] public required Guid UserId { get; set; }//The User actually reserving the place
    [Column("listingid")] public required Guid ListingId { get; set; }
    [Column("price")] public required float Price { get; set; }
    [Column("reservationdate")] public required DateTime ReservationDate { get; set; } = DateTime.Now.ToUniversalTime();
    [Column("startdate")] public required DateTime StartDate { get; set; }
    [Column("enddate")] public required DateTime EndDate { get; set; }
    [Column("reviewstar")] [Range(0, 5, ErrorMessage = "Number of stars should be between 0 and 5")] public int? Stars { get; set; }
    [Column("reviewdescription")] public string? ReviewDescription { get; set; }
}
