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

        entities.Listings? result;
        try
        {
            result = await _context.Listing.Where(x => x.Id == InfoListing.Id).Include(x => x.Images).FirstOrDefaultAsync();

            if(result is null)
                return Error.NotFound("Listing Not Found");
        } 
        
        catch (NpgsqlException exception)
        {
            return Error.Failure(exception.Message);
        }

        return new ListingInfoIntern{
            Images = result.Images.Select(x => x.ImageUri).ToList(),
            Addresse = result.Addresse,
            Price_day = result.Price_day,
            Creation_date = result.Creation_date,
            Type = result.Type,
            Average_stars = result.Average_stars,
            UserId = result.UserId,
            Name = result.Name
        };
    }

    public async Task<UserListingsResponse> UserListings(UserListingsRequest InfoListing, CancellationToken token)
    {
        UserListingsResponse? list_info = new UserListingsResponse{};

        //Add Order by date and limit 
        string sql  = $"SELECT id, country, city, name FROM listing WHERE userid = @UserId; SELECT \"ListingId\" as id, imageuri as uri FROM image WHERE \"ListingId\" IN (SELECT id FROM listing WHERE userid = @UserId);";

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);

            db.Open();

            var multi = await db.QueryMultipleAsync(sql, new {UserId = InfoListing.UserId});
            list_info.minilistings = await multi.ReadAsync<MinimalListingInfoInternal>();
            var imgs = await multi.ReadAsync<Images>();

            foreach(var l in list_info.minilistings)
            {
                foreach(var img in imgs.Where(x => x.Id == l.Id))
                {
                    l.Images.Add(img.uri);
                }
            }

            if (list_info.minilistings is null)
            {
                list_info.IsError = true;
                list_info.Message = "User doesn't have any listing";
                list_info.code = 404;
                return list_info;
            }

            db.Close();
        }
        catch (NpgsqlException exception)
        {
            list_info.IsError = true;
            list_info.Message = exception.Message;
            list_info.code = 404;
            return list_info;
        }

        return list_info;
    }
}