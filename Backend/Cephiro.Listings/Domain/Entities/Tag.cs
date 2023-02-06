using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Domain.Entities;


[Table(name: "tag", Schema = "listings")]
public sealed class Tag
{
    [Column("id")] [Required] public Guid Id {get; set;}
    [Column("tag_string")] [Required] public string? Tag_string { get; set; }
        
}
