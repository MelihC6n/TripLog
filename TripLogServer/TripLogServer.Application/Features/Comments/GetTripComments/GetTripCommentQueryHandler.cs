using MediatR;
using TripLogServer.Application.Features.Trips.GetAll;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Comments.GetTripComments;

internal sealed record GetTripCommentQueryHandler(
    ICommentRepository commentRepository) : IRequestHandler<GetTripCommentQuery, Result<List<QueryComment>>>
{
    public async Task<Result<List<QueryComment>>> Handle(GetTripCommentQuery request, CancellationToken cancellationToken)
    {
        var response = commentRepository.WhereNoTracking(x => x.TripId == request.TripId).OrderByDescending(c => c.CreatedAt).Select(c =>
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
        return response;
    }
}