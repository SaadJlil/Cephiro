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
        DbWriteInternal result = new()
        {
            ChangeCount = 0,
            Error = null
        };
        models.Listings lst = new models.Listings
        {
            Addresse = listing.Addresse,
            Description = listing.Description,
            Price_day = listing.Price_day,
            Creation_date = DateTime.Now.ToUniversalTime(),
            Type = listing.Type,
            UserId = listing.UserId,
            //Add tags later on 
            Name = listing.Name
        };
        var phts = listing.Images.Select(x => new models.Photos
        {
            Listing = lst,
            ImageUri = x
        }).ToList();
        lst.Images = phts;
        try
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            await _context.Image.AddRangeAsync(phts);
            await _context.Listing.AddAsync(lst);
            result.ChangeCount = await _context.SaveChangesAsync();
        }
        catch (DbUpdateException exception)
        {
            return new DbWriteInternal()
            {
                ChangeCount = 0,
                Error = new()
                {
                    Code = 502,
                    Message = $"Could not establish connection to the database - ${exception.Message + exception.InnerException}",
                }
            };
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



        DbWriteInternal result = new()
        {
            ChangeCount = 0,
            Error = null
        };

        //Checking if the listing belongs to the user
        string sql = $@"SELECT 1 FROM listing WHERE id = @listingid AND userid = @userid LIMIT 1";
        var param = new { Uplisting.ListingId, Uplisting.UserId };

        var cmd = new CommandDefinition(commandText: sql, parameters: param, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);
            db.Open();
            if (await db.QueryFirstOrDefaultAsync(cmd) is null)
            {
                result.Error = new Application.Shared.Contracts.Error
                {
                    Code = 404,
                    Message = "Listing doesn't exist or belong to the user"
                };
                return result;
            }
            db.Close();
        }
        catch (NpgsqlException exception)
        {
            result.Error = new Application.Shared.Contracts.Error
            {
                Code = 404,
                Message = exception.Message
            };
            return result;
        }


        //Update image and listing tables depending on what changes have been done 

        //var paramarray = new DynamicParameters();
        var paramDic = new Dictionary<string, object>();
        var updatearray = new List<string>();


        paramDic.Add("@listingid" ,Uplisting.ListingId);
        paramDic.Add("@userid", Uplisting.UserId);

        if (Uplisting.Name != null)
        {
            updatearray.Add($@" name = @uplistingname ");
            paramDic.Add("@uplistingname", Uplisting.Name);
        }

        if (Uplisting.Addresse != null)
        {
            updatearray.Add($@" street = @street ");
            updatearray.Add($@" country = @country ");
            updatearray.Add($@" city = @city ");
            updatearray.Add($@" zipcode = @zipcode ");
            paramDic.Add("@street", Uplisting.Addresse.Street);
            paramDic.Add("@country", Uplisting.Addresse.Country);
            paramDic.Add("@city", Uplisting.Addresse.City);
            paramDic.Add("@zipcode", Uplisting.Addresse.ZipCode);
            if (Uplisting.Addresse.Latitude != null && Uplisting.Addresse.Latitude != null)
            {
                updatearray.Add($@" longitude = @longitude ");
                updatearray.Add($@" latitude = @latitude ");
                paramDic.Add("@longitude", Uplisting.Addresse.Longitude);
                paramDic.Add("@latitude", Uplisting.Addresse.Latitude);
            }
        }

        if (Uplisting.Description != null)
        {
            updatearray.Add($@" description = @desc ");
            paramDic.Add("@desc", Uplisting.Description);
        }

        if (Uplisting.Type != null)
        {
            updatearray.Add($@" listing_type = @type ");
            paramDic.Add("@type", Uplisting.Type);
        }

        if (Uplisting.Price_day != null)
        {
            updatearray.Add($@" price_day = @price ");
            paramDic.Add("@price", Uplisting.Price_day);
        }
        sql = "";

        if (updatearray.Count() > 0)
        {
            sql = $@"
            UPDATE listing
            SET {updatearray.Aggregate((i, j) => i + "," + j)}
            WHERE id = @listingid AND userid = @userid;";
        }




        if (Uplisting.Images != null)
        {

            sql += $" DELETE FROM image WHERE \"ListingId\" = @listingid;";
            int i = 0;
            foreach (var image in Uplisting.Images)
            {
                sql += $"INSERT INTO image (id, \"ListingId\", imageuri) VALUES (@Id{i}, @listingid, @photo{i});";
                paramDic.Add("@Id"+i, Guid.NewGuid());
                paramDic.Add("@photo"+i, image.ToString());
                i++;
            }

        }

        cmd = new CommandDefinition(commandText: sql, parameters: paramDic, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }
        catch (NpgsqlException exception)
        {
            result.Error = new Application.Shared.Contracts.Error
            {
                Code = 404,
                Message = exception.Message
            };
            return result;
        }

        return result;
    }
}