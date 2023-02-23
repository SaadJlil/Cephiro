using System.ComponentModel.DataAnnotations;


namespace Cephiro.Listings.Application.Reservation.Contracts.Request;

public sealed class SubmitReviewRequest
{
    public required Guid UserId { get; set; }
    public required Guid ReservationId { get; set; }
    [Range(0,5, ErrorMessage = "The number of stars has to be between 0 and 5")] public required float Stars { get; set; }
    [MaxLength(500, ErrorMessage = "Review Description has to be less than 500 characters")] public string? Description { get; set; }

}