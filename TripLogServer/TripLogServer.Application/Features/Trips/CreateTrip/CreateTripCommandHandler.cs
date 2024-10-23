using MediatR;
using TripLogServer.Application.Services;
using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TS.Result;

namespace TripLogServer.Application.Features.Trips.CreateTrip;

internal sealed class CreateTripCommandHandler(
    ITripRepository tripRepository,
    ITagRepository tagRepository,
    IImageService imageService) : IRequestHandler<CreateTripCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateTripCommand request, CancellationToken cancellationToken)
    {
        string folder = "trips";
        var tagList = request.Tags.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        foreach (var tags in tagList)
        {
            if (!tagRepository.Any(t => t.Name == tags))
            {
                Tag tag = new()
                {
                    Name = tags
                };
                await tagRepository.CreateAsync(tag, cancellationToken);
            }
        }

        var tripTags = tagRepository.Where(searchTags => tagList.Any(x => x == searchTags.Name)).ToList();


        List<TripContent> tripContents = new List<TripContent>();

        var imagePath = string.Join('.', DateTime.Now.ToFileTime().ToString(), request.Image.FileName);

        Trip trip = new()
        {
            Title = request.Title,
            Description = request.Description,
            Tags = tripTags,
            ImageUrl = imagePath,
            TripContents = tripContents
        };
        await tripRepository.CreateAsync(trip, cancellationToken);

        await imageService.SaveImageAsync(imagePath, folder, request.Image);

        return "ok";
    }
}