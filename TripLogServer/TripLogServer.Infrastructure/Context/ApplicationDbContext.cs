using Microsoft.EntityFrameworkCore;
using TripLogServer.Domain.Entities;

namespace TripLogServer.Infrastructure.Context;
internal sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    DbSet<Trip> Trips { get; set; }
    DbSet<Tag> Tags { get; set; }
    DbSet<TripContent> TripContents { get; set; }
}
