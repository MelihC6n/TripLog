namespace TripLogServer.Domain.Entities;
public sealed class Trip
{
    public Trip()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<TripContent>? TripContents { get; set; }
}
