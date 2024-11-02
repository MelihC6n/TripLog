namespace TripLogServer.Domain.Entities;
public sealed class Comment
{
    public Comment()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Text { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public Guid TripId { get; set; }
    public Trip? Trip { get; set; }
    public string AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}