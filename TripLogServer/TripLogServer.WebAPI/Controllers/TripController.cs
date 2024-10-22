using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripLogServer.Application.Features.Trip.CreateTrip;
using TripLogServer.WebAPI.Abstractions;

namespace TripLogServer.WebAPI.Controllers;

public class TripController : ApiController
{
    public TripController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateTripCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
