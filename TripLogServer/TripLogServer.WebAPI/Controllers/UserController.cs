using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripLogServer.Application.Features.User.UserCreate;
using TripLogServer.WebAPI.Abstractions;

namespace TripLogServer.WebAPI.Controllers;

public class UserController : ApiController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
