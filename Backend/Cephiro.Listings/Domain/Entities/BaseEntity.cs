using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Domain.Entities;

public class BaseEntity<T> where T : notnull, new()
{
    [Key] [Required] public T Id { get; set; } = new();
}