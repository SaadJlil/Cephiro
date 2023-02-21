using Cephiro.Listings.Application.Reservation.Commands;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Shared.Contracts.Internal;
using models = Cephiro.Listings.Domain.Entities;
using Cephiro.Listings.Domain.Enums;
using Microsoft.EntityFrameworkCore;



using Dapper;
using ErrorOr;
using Microsoft.Extensions.Options;
using Npgsql;


namespace Cephiro.Listings.Infrastructure.Data.Execute;


public class ReservationExecute : IReservationExecute
{
    private readonly ListingsDbContext _context;
    private readonly IOptionsMonitor<DapperConfig> _settings;

    public ReservationExecute(ListingsDbContext context, IOptionsMonitor<DapperConfig> settings)
    {
        _settings = settings;
        _context = context;
    }

    public async Task<DbWriteInternal> CreateReservation(CreateReservationRequest reservation, CancellationToken token)
    {

        DbWriteInternal result = new()
        {
            ChangeCount = 0,
            Error = null
        };
        bool existing_reservations = await _context.Reservation.Where(x => x.ListingId == reservation.ListingId 
            && !(x.StartDate <= reservation.EndDate && x.StartDate >= reservation.StartDate)
            && !(reservation.StartDate <= x.EndDate && reservation.StartDate >= x.StartDate)
            && !(x.EndDate >= reservation.StartDate && x.EndDate <= reservation.EndDate)
            && !(reservation.EndDate >= reservation.StartDate && x.StartDate <= reservation.EndDate))
            .AnyAsync();
        

        if(existing_reservations)
        {
            return new DbWriteInternal() 
            {
                ChangeCount = 0,
                Error = new()
                {
                    Code = 404,
                    Message = "The dates indicated are already reserved"
                }
            };
        }

        string sql = $"INSERT INTO reservation (id, userid, listingid, price, startdate, enddate, reservationdate) VALUES (@Id, @UserId, @ListingId, (SELECT price_day * @Period FROM listing WHERE id = @ListingId LIMIT 1), @StartDate, @EndDate, @ReservationDate);";

        var cmd = new CommandDefinition(commandText: sql, parameters: new {Id = Guid.NewGuid(), UserId = reservation.UserId, ListingId = reservation.ListingId, Period = reservation.EndDate.Subtract(reservation.StartDate).TotalDays, StartDate = reservation.StartDate, EndDate = reservation.EndDate, ReservationDate = DateTime.Now.ToUniversalTime()}, cancellationToken: token);

        try
        {
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            using NpgsqlConnection db = new(_settings.CurrentValue.ListingsConnection);
            db.Open();
            result.ChangeCount = await db.ExecuteAsync(cmd);
            db.Close();
        }
        catch (NpgsqlException exception)
        {
            return new DbWriteInternal()
            {
                ChangeCount = 0,
                Error = new()
                {
                    Code = 502,
                    Message = exception.Message
                }
            };
        }

        return result;

    }
}