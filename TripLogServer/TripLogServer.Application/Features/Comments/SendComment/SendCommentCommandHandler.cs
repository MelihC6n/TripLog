using MediatR;
using Microsoft.AspNetCore.Identity;
using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Comments.SendComment;

internal sealed class SendCommentCommandHandler(
    UserManager<AppUser> userManager,
    ICommentRepository commentRepository) : IRequestHandler<SendCommentCommand, Result<string>>
{
    public async Task<Result<string>> Handle(SendCommentCommand request, CancellationToken cancellationToken)
    {
        var user = userManager.Users.FirstOrDefault(x => x.Id == request.appUserId);
        if (user == null)
        {
            return Result<string>.Failure("Kullanıcı bilgileri alınamadı, lütfen tekrar giriş yapınız!");
        }

        Comment comment = new()
        {
            AppUserId = request.appUserId,
            TripId = request.tripId,
            Text = request.Text
        };

        await commentRepository.CreateAsync(comment, cancellationToken);
        return "Değerli görüşünüz gezi sahibiyle paylaşıldı!";
    }
}
