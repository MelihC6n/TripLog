using MediatR;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.GetAll;

internal sealed class GetAllTripQueryHandler(
    ITripRepository tripRepository) : IRequestHandler<GetAllTripQuery, Result<List<GetAllTripQueryResponse>>>
{
    public async Task<Result<List<GetAllTripQueryResponse>>> Handle(GetAllTripQuery request, CancellationToken cancellationToken)
    {
        var response = tripRepository.GetAllTripWithContents().OrderByDescending(x => x.CreatedDate).ToList().Select(t =>
            new GetAllTripQueryResponse
            (
                t.Id,
                t.Title,
                t.Description,
                t.ImageUrl,
                t.CreatedDate,
                t.AppUserId,

                t.Tags.Select(x => new QueryTags
                (
                    x.Id,
                    x.Name
                )).ToList(),

                t.TripContents.Select(z => new QueryTripContents
                (
                    z.Id,
                    z.Title,
                    z.Description,
                    z.ImageUrl
                )).ToList(),

                new QueryAppUser
                (
                    t.AppUser.Id,
                    t.AppUser.Email,
                    t.AppUser.UserName,
                    t.AppUser.IsAuthor
                ),

                t.Comments.Select(c => new QueryComment
                (
                    c.Id,
                    c.Text,
                    c.CreatedAt,
                    new QueryAppUser
                    (
                        c.AppUser.Id,
                        c.AppUser.Email,
                        c.AppUser.UserName,
                        c.AppUser.IsAuthor
                    )
                )).ToList().OrderByDescending(c => c.CreatedAt)

            )).ToList();

        return response;
    }
}