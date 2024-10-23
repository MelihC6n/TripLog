using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.CreateTrip;
public sealed record CreateTripCommand(
    string Title,
    string Description,
    IFormFile Image,
    string Tags,
    List<TripContentCommand> TripContents) : IRequest<Result<string>>;

public sealed record TripContentCommand(
    string Title,
    string Description,
    IFormFile Image);