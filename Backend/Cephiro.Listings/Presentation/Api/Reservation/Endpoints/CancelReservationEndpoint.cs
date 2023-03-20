using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

[Route("/cancelreservation")]
public sealed class CancelReservationEndpoint: EndpointBaseAsync
    .WithRequest<CancelReservationRequest>
    .WithActionResult<CancelReservationResponse>
{
    private readonly IRequestClient<CancelReservationRequest> _cancel;
    public CancelReservationEndpoint(IRequestClient<CancelReservationRequest> cancel)
    {
        _cancel = cancel;
    }

    [HttpPost]
    public override async Task<ActionResult<CancelReservationResponse>> HandleAsync([FromBody] CancelReservationRequest reservation, CancellationToken cancellationToken = default)
    {
        var response = await _cancel.GetResponse<CancelReservationResponse>(reservation);
        return response.Message;
    }
}

