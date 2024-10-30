using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.UpdateTrip;
public sealed record UpdateTripCommand(
    Guid Id,
    string Title,
    string Description,
    IFormFile? Image,
    string Tags,
    List<UpdateTripContent> TripContents) : IRequest<Result<string>>;

public sealed record UpdateTripContent(
    Guid? Id,
    string Title,
    string Description,
    IFormFile? Image);
