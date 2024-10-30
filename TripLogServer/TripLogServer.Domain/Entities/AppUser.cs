using Microsoft.AspNetCore.Identity;

namespace TripLogServer.Domain.Entities;
public sealed class AppUser : IdentityUser
{
    public bool IsAuthor { get; set; }
}
