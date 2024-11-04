using MediatR;
using TS.Result;

namespace TripLogServer.Application.Features.Comments.SendComment;
public sealed record SendCommentCommand(
    string appUserId,
    Guid tripId,
    string Text) : IRequest<Result<string>>;
