using Microsoft.EntityFrameworkCore;
using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TripLogServer.Infrastructure.Abstractions;
using TripLogServer.Infrastructure.Context;

namespace TripLogServer.Infrastructure.Repositories;
internal sealed class TripRepository : Repository<Trip, ApplicationDbContext>, ITripRepository
{
    private readonly DbSet<Trip> _context;
    public TripRepository(ApplicationDbContext context) : base(context)
    {
        _context = context.Set<Trip>();
    }

    public IQueryable<Trip> GetAllTripWithContents()
    {
        return _context.Include(t => t.TripContents).Include(t => t.Tags).Select(t =>
        new Trip
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            ImageUrl = t.ImageUrl,
            CreatedDate = t.CreatedDate,

            Tags = t.Tags.Select(x => new Tag
            {
                Id = x.Id,
                Name = x.Name
            }).ToList(),

            TripContents = t.TripContents.Select(z => new TripContent
            {
                Id = z.Id,
                Title = z.Title,
                Description = z.Description,
                ImageUrl = z.ImageUrl
            }).ToList()

        }).AsQueryable();
    }
}
