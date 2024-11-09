namespace TripLogServer.Application.Features.Trips.GetAll;
public sealed record GetAllTripQueryResponse(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl,
    DateTime CreatedDate,
    string AppUserId,
    List<QueryTags> Tags,
    QueryAppUser AppUser
    );

public sealed record QueryTags(
    Guid Id,
    string Name);

public sealed record QueryAppUser(
     string Id,
     string Email,
     string UserName,
     bool IsAuthor);