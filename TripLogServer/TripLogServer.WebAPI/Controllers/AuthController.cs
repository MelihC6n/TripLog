using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripLogServer.Application.Features.Auth.Login;
using TripLogServer.WebAPI.Abstractions;

namespace TripLogServer.WebAPI.Controllers;

public class AuthController : ApiController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
