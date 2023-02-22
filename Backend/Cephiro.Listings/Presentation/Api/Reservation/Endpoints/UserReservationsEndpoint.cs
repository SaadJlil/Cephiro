using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

[Route("/userreservations")]
public sealed class UserReservationsEndpoint: EndpointBaseAsync
    .WithRequest<UserReservationsRequest>
    .WithActionResult<UserReservationsResponse>
{
    private readonly IRequestClient<UserReservationsRequest> _usrres;
    public UserReservationsEndpoint(IRequestClient<UserReservationsRequest> usrres)
    {
        _usrres = usrres;
    }

    [HttpPost]
    public override async Task<ActionResult<UserReservationsResponse>> HandleAsync([FromBody] UserReservationsRequest rsv, CancellationToken cancellationToken = default)
    {
        var response = await _usrres.GetResponse<UserReservationsResponse>(rsv);
        return response.Message;
    }
}

