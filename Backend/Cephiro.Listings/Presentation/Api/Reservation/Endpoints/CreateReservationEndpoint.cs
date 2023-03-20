using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

[Route("/createreservation")]
public sealed class CreateReservationEndpoint : EndpointBaseAsync
    .WithRequest<CreateReservationRequest>
    .WithActionResult<CreateReservationResponse>
{
    private readonly IRequestClient<CreateReservationRequest> _create;
    public CreateReservationEndpoint(IRequestClient<CreateReservationRequest> create)
    {
        _create = create;
    }

    [HttpPost]
    public override async Task<ActionResult<CreateReservationResponse>> HandleAsync([FromBody] CreateReservationRequest reservation, CancellationToken cancellationToken = default)
    {
        var response = await _create.GetResponse<CreateReservationResponse>(reservation);
        return response.Message;
    }
}

