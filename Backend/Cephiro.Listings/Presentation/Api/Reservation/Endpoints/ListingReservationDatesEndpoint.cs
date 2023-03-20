using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

[Route("/listingreservationdates")]
public sealed class ListingReservationDatesEndpoint: EndpointBaseAsync
    .WithRequest<ListingReservationDatesRequest>
    .WithActionResult<ListingReservationDatesResponse>
{
    private readonly IRequestClient<ListingReservationDatesRequest> _listingdates;
    public ListingReservationDatesEndpoint(IRequestClient<ListingReservationDatesRequest> listingdates)
    {
        _listingdates = listingdates;
    }

    [HttpPost]
    public override async Task<ActionResult<ListingReservationDatesResponse>> HandleAsync([FromBody] ListingReservationDatesRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _listingdates.GetResponse<ListingReservationDatesResponse>(request);
        return response.Message;
    }
}

