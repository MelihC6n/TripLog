namespace TripLogServer.Domain.Entities;
public sealed class TripContent
{
    public TripContent()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public Trip? Trip { get; set; }
    public Guid TripId { get; set; }
}
