using MediatR;
using TripLogServer.Application.Features.Trips.GetAll;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.GetContentOfTrip;
public sealed record GetContentOfTripQuery(
    Guid TripId) : IRequest<Result<GetContentOfTripResponse>>;

public sealed record GetContentOfTripResponse(
    List<QueryTripContents> TripContents,
    List<QueryComment> Comments
    );

internal sealed class GetContentOfTripHandler(
    ITripContentRepository tripContentRepository,
    ICommentRepository commentRepository) : IRequestHandler<GetContentOfTripQuery, Result<GetContentOfTripResponse>>
{
    public async Task<Result<GetContentOfTripResponse>> Handle(GetContentOfTripQuery request, CancellationToken cancellationToken)
    {
        var contents = tripContentRepository.WhereNoTracking(t => t.TripId == request.TripId)
            .Select(
            x => new QueryTripContents(x.Id, x.Title, x.Description, x.ImageUrl)).ToList();

        var comments = commentRepository.WhereNoTracking(x => x.TripId == request.TripId).OrderByDescending(c => c.CreatedAt).Select(c =>
            new QueryComment(
                c.Id,
                c.Text,
                c.CreatedAt,
                new QueryAppUser(
                    c.AppUser.Id,
                    c.AppUser.Email,
                    c.AppUser.UserName,
                    c.AppUser.IsAuthor
                ))).ToList();

        var response = new GetContentOfTripResponse(contents, comments);
        return response;
    }
}

public sealed record QueryTripContents(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl);

public sealed record QueryComment(
     Guid Id,
     string Text,
     DateTime CreatedAt,
     QueryAppUser AppUser);