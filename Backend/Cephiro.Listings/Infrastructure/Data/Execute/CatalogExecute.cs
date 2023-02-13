using Cephiro.Listings.Application.Catalog.Commands;
using Cephiro.Listings.Application.Catalog.Contracts.Request;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using models = Cephiro.Listings.Domain.Entities;
using Cephiro.Listings.Domain.Enums;
using Microsoft.EntityFrameworkCore;



using Dapper;
using ErrorOr;
using Microsoft.Extensions.Options;
using Npgsql;




namespace Cephiro.Listings.Infrastructure.Data.Execute;


public class CatalogExecute : ICatalogExecute
{
    private readonly ListingsDbContext _context;
    private readonly IOptionsMonitor<DapperConfig> _settings;

    public CatalogExecute(ListingsDbContext context, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
        _context = context;
    }
    public async Task<DbWriteInternal> CreateListing(CreationRequest listing, CancellationToken token)
    {
        DbWriteInternal result = new() {
            ChangeCount = 0,
            Error = null
        };
        models.Listings lst = new models.Listings{
            Addresse = listing.Addresse,
            Description = listing.Description,
            Price_day = listing.Price_day,
            Creation_date = DateTime.Now,
            Type = listing.Type,
            UserId = listing.UserId,
            //Add tags later on 
            Name = listing.Name
        };
        var phts = listing.Images.Select(x => new models.Photos{
            Listing = lst,
            Image = x
        }).ToList();
        lst.Images = phts;
        try{
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            await _context.Image.AddRangeAsync(phts);
            await _context.Listing.AddAsync(lst);
            result.ChangeCount = await _context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            return new DbWriteInternal() 
            { 
                ChangeCount = 0, Error = new() 
                {
                    Code = 502, 
                    Message = $"Could not establish connection to the database - ${exception.Message + exception.InnerException}",
                }};
        }

        finally
        {
            _context.ChangeTracker.Clear();
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        return result;
    }
    public async Task<DbWriteInternal> UpdateListing(UpdateListingRequest Uplisting, CancellationToken token)
    {
        DbWriteInternal result = new() {
            ChangeCount = 0,
            Error = null
        };

   }
}