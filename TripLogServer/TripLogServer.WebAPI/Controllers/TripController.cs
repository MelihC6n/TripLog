using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripLogServer.Application.Features.Trips.CreateTrip;
using TripLogServer.Application.Features.Trips.DeleteTrip;
using TripLogServer.Application.Features.Trips.GetAll;
using TripLogServer.Application.Features.Trips.GetContentOfTrip;
using TripLogServer.Application.Features.Trips.GetFromTag;
using TripLogServer.Application.Features.Trips.UpdateTrip;
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

    [HttpPost]
    public async Task<IActionResult> GetAll(GetAllTripQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetFromTag(GetFromTagQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateTripCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetContentOfTrip(GetContentOfTripQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
