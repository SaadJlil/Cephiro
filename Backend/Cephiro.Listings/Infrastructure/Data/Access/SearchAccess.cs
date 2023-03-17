using Cephiro.Listings.Application.Search;
using entities = Cephiro.Listings.Domain.Entities;
using Cephiro.Listings.Application.Search.Queries;
using Cephiro.Listings.Application.Search.Contracts.Enums;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Search.Contracts.Request;
using Cephiro.Listings.Application.Search.Contracts.Response;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
using Dapper;
using Npgsql;


namespace Cephiro.Listings.Infrastructure.Data.Access;

public class SearchAccess: ISearchAccess 
{
    private readonly ListingsDbContext _context;
    private readonly IOptionsMonitor<DapperConfig> _settings;

    public SearchAccess(ListingsDbContext context, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
        _context = context;
    }


    public async Task<ListingSearchResponse> GetListingSearch(ListingSearchRequest Search, CancellationToken token)
    {

        ListingSearchResponse? list_info = new ListingSearchResponse{};

        var paramDic = new Dictionary<string, object>();
        var listingarray = new List<string>();
        var reservationarray = new List<string>();
        string orderby = "";


        paramDic.Add("@take", Search.take);
        paramDic.Add("@skip", Search.skip);

        //Country
        listingarray.Add($@" country = @Country ");
        paramDic.Add("@Country", Search.Country);

        //Start
        paramDic.Add("@StartDate", Search.StartDate);
        
        //EndDate
        paramDic.Add("@EndDate", Search.EndDate);


        if (Search.Type != null)
        {
            listingarray.Add($@" listing_type = @ListingType ");
            paramDic.Add("@ListingType", Search.Type);
        }

        if (Search.City != null)
        {
            listingarray.Add($@" city = @City ");
            paramDic.Add("@City", Search.City);
        }

        if(Search.MinimumPrice != null)
        {
            listingarray.Add($@" price_day >= @MinimumPrice ");
            paramDic.Add("@MinimumPrice", Search.MinimumPrice);
        }

        if(Search.MaximumPrice != null)
        {
            listingarray.Add($@" price_day <= @MaximumPrice ");
            paramDic.Add("@MaximumPrice", Search.MaximumPrice);
        }

        if(Search.SurfaceMin != null)
        {
            listingarray.Add($@" surface >= @MinSurface ");
            paramDic.Add("@MinSurface", Search.SurfaceMin);
        }

        if(Search.SurfaceMax != null)
        {
            listingarray.Add($@" surface <= @MaxSurface");
            paramDic.Add("@MaxSurface", Search.SurfaceMax);
        }

        if(Search.Beds != null)
        {
            listingarray.Add($@" beds = @Beds ");
            paramDic.Add("@Beds", Search.Beds);
        }

        if(Search.Bedrooms != null)
        {
            listingarray.Add($@" bedrooms = @Bedrooms ");
            paramDic.Add("@Bedrooms", Search.Bedrooms);
        }

        if(Search.Bathrooms != null)
        {
            listingarray.Add($@" bathrooms = @Bathrooms ");
            paramDic.Add("@Bathrooms", Search.Bathrooms);
        }

        if(Search.Wifi != null && Search.Wifi == true)
        {
            listingarray.Add($@" wifi = @Wifi ");
            paramDic.Add("@Wifi", Search.Wifi);
        }

        if(Search.AirConditioning != null && Search.AirConditioning == true)
        {
            listingarray.Add($@" airconditioning = @AirConditioning ");
            paramDic.Add("@AirConditioning", Search.AirConditioning);
        }

        if(Search.Smoking != null && Search.Smoking == true)
        {
            listingarray.Add($@" smoking = @Smoking ");
            paramDic.Add("@Smoking", Search.Smoking);
        }

        if(Search.WashingMachine != null && Search.WashingMachine == true)
        {
            listingarray.Add($@" washing_machine = @WashingMachine ");
            paramDic.Add("@WashingMachine", Search.WashingMachine);
        }

        if(Search.DishWasher != null && Search.DishWasher == true)
        {
            listingarray.Add($@" dish_washer = @DishWasher ");
            paramDic.Add("@DishWasher", Search.DishWasher);
        }



        switch(Search.OrderBy)
        {
            case PreferenceOrderBy.OLDEST:
                orderby = "ORDER BY creation_date"; 
                break;

            case PreferenceOrderBy.PRICE:
                orderby = "ORDER BY price_day";
                break;
        }

        if(Search.QueryString is not null)
        {
            paramDic.Add("@SearchQuery", Search.QueryString);
            if(Search.OrderBy is null)
            {
                orderby = "ORDER BY search @@ websearch_to_tsquery('english', @SearchQuery) or search @@ websearch_to_tsquery('simple', @SearchQuery) DESC";
            }
            else
                orderby += ", search @@ websearch_to_tsquery('english', @SearchQuery) or search @@ websearch_to_tsquery('simple', @SearchQuery) DESC";
        }

        listingarray.Add($@" 0 = (CASE WHEN EXISTS (SELECT 1 FROM reservation WHERE listing.id = reservation.listingid AND ((reservation.startdate BETWEEN @StartDate AND @EndDate) OR (reservation.enddate BETWEEN @StartDate AND @EndDate) OR (@StartDate BETWEEN reservation.startdate AND reservation.enddate) OR  (@EndDate BETWEEN reservation.startdate AND reservation.enddate))) THEN 1 ELSE 0 END) ");

        string sql  = $"SELECT id, country, city, name, price_day as price, average_stars as stars FROM listing WHERE {listingarray.Aggregate((i, j) => i + "AND" + j)} {orderby} LIMIT @take OFFSET @skip * @take ; SELECT \"ListingId\" as id, imageuri as uri FROM image WHERE \"ListingId\" IN (SELECT id FROM listing WHERE {listingarray.Aggregate((i, j) => i + "AND" + j)} {orderby} LIMIT @take OFFSET @skip * @take);"; 
//DECLARE @variable_name etc.. 

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);

            db.Open();

            var multi = await db.QueryMultipleAsync(sql, paramDic);
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
                list_info.Message = "No Listings Found";
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
