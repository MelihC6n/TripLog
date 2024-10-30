using MediatR;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.DeleteTrip;
public sealed record DeleteTripCommand(
    Guid Id) : IRequest<Result<string>>;
