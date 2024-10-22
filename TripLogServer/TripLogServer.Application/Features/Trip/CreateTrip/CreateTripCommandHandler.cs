using MediatR;
using TS.Result;

namespace TripLogServer.Application.Features.Trip.CreateTrip;

internal sealed class CreateTripCommandHandler() : IRequestHandler<CreateTripCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(request);
        return "ok";
    }
}