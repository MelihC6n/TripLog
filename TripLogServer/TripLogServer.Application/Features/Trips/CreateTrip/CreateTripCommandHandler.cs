﻿using MediatR;
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

        var tripTags = tagRepository.Where(searchTags => tagList.Any(x => x == searchTags.Name)).ToList();

        List<TripContent> tripContents = new List<TripContent>();

        foreach (var item in request.TripContents)
        {
            var contentImagePath = string.Join('.', DateTime.Now.ToFileTime().ToString(), item.Image.FileName);

            TripContent tripContent = new()
            {
                Title = item.Title,
                Description = item.Description,
                ImageUrl = contentImagePath
            };

            await imageService.SaveImageAsync(contentImagePath, "contents", item.Image);

            tripContents.Add(tripContent);
        }

        var imagePath = string.Join('.', DateTime.Now.ToFileTime().ToString(), request.Image.FileName);

        Trip trip = new()
        {
            Title = request.Title,
            Description = request.Description,
            Tags = tripTags,
            ImageUrl = imagePath,
            AppUserId = request.AppUserId,
            TripContents = tripContents
        };
        await tripRepository.CreateAsync(trip, cancellationToken);

        await imageService.SaveImageAsync(imagePath, "trips", request.Image);

        return "ok";
    }
}