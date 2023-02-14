using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cephiro.Listings.Domain.Entities;

public class BaseEntity<T> where T : notnull, new()
{
    [Key] [Required] [Column("id")] public T Id { get; set; } = new();
}