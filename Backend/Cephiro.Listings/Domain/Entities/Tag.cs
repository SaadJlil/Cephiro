using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Domain.Entities;


[Table(name: "tag")]
public sealed class Tag: BaseEntity<Guid>
{
    [Column("tag_string")] [Required] public string? Tag_string { get; set; }
        
}
