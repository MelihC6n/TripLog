using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripLogServer.Application.Features.Comments.GetTripComments;
using TripLogServer.Application.Features.Comments.SendComment;
using TripLogServer.WebAPI.Abstractions;

namespace TripLogServer.WebAPI.Controllers;

public sealed class CommentController : ApiController
{
    public CommentController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Send(SendCommentCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost]
    public async Task<IActionResult> GetTripComments(GetTripCommentQuery request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
