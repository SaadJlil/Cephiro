namespace Cephiro.Listings.Application.Shared.Contracts.Internal;


public sealed class MinimalListingInfoInternal
{
    public Guid Id { get; set; }
    public required List<string> Images{ get; set; } = new List<string>();//Add list 
    public required string Country { get; set; }
    public required string City{ get; set; }
    public required string Name { get; set; }
    public required int Price { get; set; }
    public float? Stars { get; set; }
}


public sealed class Images
{
    public required Guid Id { get; set; }
    public required string uri { get; set; }
}