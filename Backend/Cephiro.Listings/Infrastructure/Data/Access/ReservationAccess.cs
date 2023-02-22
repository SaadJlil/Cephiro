using Cephiro.Listings.Application.Reservation;
using entities = Cephiro.Listings.Domain.Entities;
using Cephiro.Listings.Application.Reservation.Queries;
using Cephiro.Listings.Domain.ValueObjects;
using Cephiro.Listings.Domain.Enums;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using ErrorOr;
using Dapper;
using Npgsql;


namespace Cephiro.Listings.Infrastructure.Data.Access;

public class ReservationAccess: IReservationAccess
{
    private readonly ListingsDbContext _context;
    private readonly IOptionsMonitor<DapperConfig> _settings;

    public  ReservationAccess(ListingsDbContext context, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
        _context = context;
    }

    public async Task<ListingReservationDatesResponse> GetListingReservations(ListingReservationDatesRequest Listing, CancellationToken token)
    {
        ListingReservationDatesResponse? list_dates = new ListingReservationDatesResponse{};

        //Add Order by date and limit 
        string sql  = $"SELECT startdate, enddate FROM reservation WHERE listingid = @ListingId AND enddate > @Today ORDER BY startdate;"; 

        var cmd = new CommandDefinition(commandText: sql, parameters: new { ListingId = Listing.ListingId, Today = DateTime.Now.ToUniversalTime() }, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);

            db.Open();

            list_dates.ListDates = await db.QueryAsync<ListingReservationDatesInternal>(cmd);

            db.Close();
        }
        catch (NpgsqlException exception)
        {
            list_dates.IsError = true;
            list_dates.Message = exception.Message;
            list_dates.Code = 404;
            return list_dates;
        }

        return list_dates;
    }

    public async Task<UserReservationsResponse> GetUserReservations(UserReservationsRequest reservation, CancellationToken token)
    {
        UserReservationsResponse? list_reservs = new UserReservationsResponse{};

        //Add Order by date and limit 
        string sql  = $@"SELECT listing.name,
                        listing.city,
                        listing.country,
                        listing.id as listingid,
                        reservation.price as invoicevalue,
                        reservation.reservationdate as reservationdate,
                        reservation.id as reservationid
                        FROM reservation
                        INNER JOIN listing 
                        ON reservation.listingid = listing.id
                        WHERE listing.userid = @UserId
                        ORDER BY reservation.startdate;"; 

        var cmd = new CommandDefinition(commandText: sql, parameters: new { UserId = reservation.UserId }, cancellationToken: token);

        try
        {
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);

            db.Open();

            list_reservs.ListReservations = await db.QueryAsync<UserReservationsInternal>(cmd);

            db.Close();
        }
        catch (NpgsqlException exception)
        {
            list_reservs.IsError = true;
            list_reservs.Message = exception.Message;
            list_reservs.Code = 404;
            return list_reservs;
        }

        return list_reservs;
    }

}