using Cephiro.Listings.Application.Catalog;
using entities = Cephiro.Listings.Domain.Entities;
using Cephiro.Listings.Application.Catalog.Queries;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Catalog.Contracts.Response;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
using Dapper;
using Npgsql;


namespace Cephiro.Listings.Infrastructure.Data.Access;

public class CatalogAccess: ICatalogAccess
{
    private readonly ListingsDbContext _context;
    private readonly IOptionsMonitor<DapperConfig> _settings;

    public CatalogAccess(ListingsDbContext context, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
        _context = context;
    }


    public async Task<ErrorOr<ListingInfoIntern>> GetListingInfo(ListingInfoRequest InfoListing, CancellationToken token)
    {

        entities.Listings result;
        try
        {
            result = _context.Listing.Where(x => x.Id == InfoListing.Id).Include(x => x.Images).FirstOrDefault();

            if(result is null)
                return Error.NotFound("Listing Not Found");
        } 
        
        catch (NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        var thing = new ListingInfoIntern{
            Images = result.Images.Select(x => x.ImageUri).ToList(),
            Addresse = result.Addresse,
            Price_day = result.Price_day,
            Creation_date = result.Creation_date,
            Type = result.Type,
            Average_stars = result.Average_stars,
            UserId = result.UserId,
            Name = result.Name
        };

        return thing;
    }
}