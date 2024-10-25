using MediatR;
using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.GetAll;

internal sealed class GetAllTripQueryHandler(
    ITripRepository tripRepository) : IRequestHandler<GetAllTripQuery, Result<List<Trip>>>
{
    public async Task<Result<List<Trip>>> Handle(GetAllTripQuery request, CancellationToken cancellationToken)
    {
        var response = tripRepository.GetAllTripWithContents().OrderByDescending(x => x.CreatedDate).ToList();

        return response;
    }
}