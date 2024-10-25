using MediatR;
using TripLogServer.Domain.Entities;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.GetFromTag;
public sealed record GetFromTagQuery(
    string TagName) : IRequest<Result<List<Trip>>>;
