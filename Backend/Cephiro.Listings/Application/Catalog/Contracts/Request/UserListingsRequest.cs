using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class UserListingsRequest 
{
    public int take { get; set; } = 1;
    public int skip { get; set; } = 0;
    public required Guid UserId{ get; set; }
}