using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class SubmitReviewRequest
{
    public required Guid UserId { get; set; }
    public required Guid ReservationId { get; set; }
    public required float Stars { get; set; }
    public string? Description { get; set; }

}