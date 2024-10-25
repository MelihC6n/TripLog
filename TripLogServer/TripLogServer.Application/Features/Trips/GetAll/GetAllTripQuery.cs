using MediatR;
using TripLogServer.Domain.Entities;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.GetAll;
public sealed record GetAllTripQuery() : IRequest<Result<List<Trip>>>;
