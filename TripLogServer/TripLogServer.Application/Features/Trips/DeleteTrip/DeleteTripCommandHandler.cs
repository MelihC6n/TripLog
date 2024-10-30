using MediatR;
using TripLogServer.Application.Services;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.DeleteTrip;

internal sealed class DeleteTripCommandHandler(
    ITripRepository tripRepository,
    IImageService imageService) : IRequestHandler<DeleteTripCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
    {
        var trip = tripRepository.FirstOrDefault(x => x.Id == request.Id);
        if (trip == null)
        {
            return Result<string>.Failure("Gezi bulunamadı!");
        }

        await tripRepository.DeleteAsync(trip, cancellationToken);
        foreach (var content in trip.TripContents)
        {
            await imageService.DeleteImageAsync(content.ImageUrl, "contents");
        }
        await imageService.DeleteImageAsync(trip.ImageUrl, "trips");
        return trip.Title + " gezisi başarıyla silindi!";
    }
}
