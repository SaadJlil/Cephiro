using System.ComponentModel.DataAnnotations;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;


namespace Cephiro.Listings.Application.Catalog.Contracts.Request;

public sealed class DeleteListingRequest 
{
    public required Guid ListingId{ get; set; }
    public required Guid UserId{ get; set; }
}