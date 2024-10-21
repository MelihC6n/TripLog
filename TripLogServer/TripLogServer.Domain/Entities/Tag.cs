namespace TripLogServer.Domain.Entities;
public sealed class Tag
{

    public Tag()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Trip>? Trips { get; set; }
}
