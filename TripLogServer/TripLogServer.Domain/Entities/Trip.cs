namespace TripLogServer.Domain.Entities;
public sealed class Trip
{
    public Trip()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<TripContent>? TripContents { get; set; }
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
