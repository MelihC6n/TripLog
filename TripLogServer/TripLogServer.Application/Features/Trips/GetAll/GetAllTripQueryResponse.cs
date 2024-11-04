namespace TripLogServer.Application.Features.Trips.GetAll;
public sealed record GetAllTripQueryResponse(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl,
    DateTime CreatedDate,
    string AppUserId,
    List<QueryTags> Tags,
    List<QueryTripContents> TripContents,
    QueryAppUser AppUser,
    IOrderedEnumerable<QueryComment> Comments);

public sealed record QueryTags(
    Guid Id,
    string Name);

public sealed record QueryTripContents(
    Guid Id,
    string Title,
    string Description,
    string ImageUrl);

public sealed record QueryAppUser(
     string Id,
     string Email,
     string UserName,
     bool IsAuthor);

public sealed record QueryComment(
     Guid Id,
     string Text,
     DateTime CreatedAt,
     QueryAppUser AppUser);