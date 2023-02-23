using Ardalis.ApiEndpoints;
using Cephiro.Listings.Application.Reservation.Contracts.Request;
using Cephiro.Listings.Application.Reservation.Contracts.Response;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

namespace Cephiro.Listings.Presentation.Api.Reservation.Endpoints;

[Route("/submitreview")]
public sealed class SubmitReviewEndpoint: EndpointBaseAsync
    .WithRequest<SubmitReviewRequest>
    .WithActionResult<SubmitReviewResponse>
{
    private readonly IRequestClient<SubmitReviewRequest> _cancel;
    public SubmitReviewEndpoint(IRequestClient<SubmitReviewRequest> cancel)
    {
        _cancel = cancel;
    }

    [HttpPost]
    public override async Task<ActionResult<SubmitReviewResponse>> HandleAsync([FromBody] SubmitReviewRequest reservation, CancellationToken cancellationToken = default)
    {
        var response = await _cancel.GetResponse<SubmitReviewResponse>(reservation);
        return response.Message;
    }
}

