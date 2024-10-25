using MediatR;
using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.GetFromTag;

internal sealed class GetFromTagQueryHandler(
    ITripRepository tripRepository,
    ITagRepository tagRepository) : IRequestHandler<GetFromTagQuery, Result<List<Trip>>>
{
    public async Task<Result<List<Trip>>> Handle(GetFromTagQuery request, CancellationToken cancellationToken)
    {
        var result = tripRepository.GetAllTripWithContents().Where(x => x.Tags.Any(t => t.Name == request.TagName)).OrderByDescending(x => x.CreatedDate).ToList();

        return result;
    }
}
