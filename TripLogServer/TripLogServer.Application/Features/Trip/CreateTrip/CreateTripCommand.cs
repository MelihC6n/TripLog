using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace TripLogServer.Application.Features.Trip.CreateTrip;
public sealed record CreateTripCommand(
    string Title,
    string Description,
    IFormFile Image,
    string Tags) : IRequest<Result<string>>;
