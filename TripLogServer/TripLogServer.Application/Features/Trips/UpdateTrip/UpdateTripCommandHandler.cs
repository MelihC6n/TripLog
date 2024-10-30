using MediatR;
using TripLogServer.Application.Services;
using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.UpdateTrip;

internal sealed class UpdateTripCommandHandler(
    ITagRepository tagRepository,
    ITripContentRepository tripContentRepository,
    ITripRepository tripRepository,
    IImageService imageService) : IRequestHandler<UpdateTripCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
    {
        //using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        //{


        var tagList = request.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (var tags in tagList)
        {
            if (!tagRepository.Any(t => t.Name.ToLower() == tags.ToLower()))
            {
                Tag tag = new()
                {
                    Name = tags.ToLower()
                };
                await tagRepository.CreateAsync(tag, cancellationToken);
            }
        }
        var tripTags = tagRepository.WhereNoTracking(searchTags => tagList.Any(x => x.ToLower() == searchTags.Name.ToLower())).ToList();

        Trip? trip = tripRepository.FirstOrDefault(t => t.Id == request.Id);
        if (trip != null)
        {
            var existingTagIds = trip.Tags.Select(x => x.Id).ToList();
            var updatedTagIds = tripTags.Select(x => x.Id).ToList();
            var tagIdsToAdd = updatedTagIds.Except(existingTagIds);
            var tagIdsToRemove = existingTagIds.Except(updatedTagIds);

            if (tagIdsToRemove.Any())
            {
                var tagsToRemove = trip.Tags.Where(x => tagIdsToRemove.Contains(x.Id)).ToList();
                foreach (var tag in tagsToRemove)
                {
                    trip.Tags.Remove(tag);
                }
            }

            if (tagIdsToAdd.Any())
            {
                var tagsToAdd = tripTags.Where(x => tagIdsToAdd.Contains(x.Id)).ToList();
                foreach (var tag in tagsToAdd)
                {
                    trip.Tags.Add(tag);
                }
            }

            trip.Title = request.Title;
            trip.Description = request.Description;
            trip.CreatedDate = DateTime.Now;

            if (request.Image != null)
            {
                var contentImagePath = string.Join('.', DateTime.Now.ToFileTime().ToString(), request.Image.FileName);
                await imageService.SaveImageAsync(contentImagePath, "trips", request.Image);
                await imageService.DeleteImageAsync(trip.ImageUrl, "trips");
                trip.ImageUrl = contentImagePath;
            }

            foreach (var content in trip.TripContents)
            {
                UpdateTripContent updateTrip = request.TripContents.FirstOrDefault(cnt => cnt.Id == content.Id);

                content.Title = updateTrip.Title;
                content.Description = updateTrip.Description;
                if (updateTrip.Image != null)
                {
                    var contentImagePath = string.Join('.', DateTime.Now.ToFileTime().ToString(), updateTrip.Image.FileName);
                    await imageService.SaveImageAsync(contentImagePath, "contents", updateTrip.Image);
                    await imageService.DeleteImageAsync(content.ImageUrl, "contets");
                    content.ImageUrl = contentImagePath;
                }
            }

            List<UpdateTripContent> addContents = request.TripContents.Where(tc => tc.Id == null).ToList();
            foreach (var item in addContents)
            {
                var contentImagePath = string.Join('.', DateTime.Now.ToFileTime().ToString(), item.Image.FileName);
                await imageService.SaveImageAsync(contentImagePath, "contents", item.Image);

                TripContent tripContent = new()
                {
                    Title = item.Title,
                    Description = item.Description,
                    ImageUrl = contentImagePath,
                    Trip = trip
                };

                await tripContentRepository.CreateAsync(tripContent, cancellationToken);
            }

            await tripRepository.UpdateAsync(trip, cancellationToken);
        }

        //    transaction.Complete();
        //}
        return "Ok";
    }
}
