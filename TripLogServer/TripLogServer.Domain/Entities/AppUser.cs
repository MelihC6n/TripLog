using Microsoft.AspNetCore.Identity;

namespace TripLogServer.Domain.Entities;
public sealed class AppUser : IdentityUser
{
    public bool IsAuthor { get; set; }
    public ICollection<Trip>? Trips { get; set; }
    public ICollection<Comment>? Comments { get; set; }
}
