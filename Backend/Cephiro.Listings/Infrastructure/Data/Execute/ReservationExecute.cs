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

    }
}