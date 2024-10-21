using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TripLogServer.Infrastructure.Abstractions;
using TripLogServer.Infrastructure.Context;

namespace TripLogServer.Infrastructure.Repositories;
internal sealed class TagRepository : Repository<Tag, ApplicationDbContext>, ITagRepository
{
    //ApplicationDbContext _context;
    public TagRepository(ApplicationDbContext context) : base(context)
    {
        // _context = context;
    }

    //public async Task<Tag> GetTagTrips(TagDTO tagDTO, CancellationToken cancellationToken)
    //{
    //    var theTag = await _context.Tags
    //        .Include(t => t.Trips).ThenInclude(tr => tr.Tags)
    //        .Include(t => t.Trips).ThenInclude(tr => tr.TripPhotos)
    //        .FirstOrDefaultAsync(tag => tag.Name == tagDTO.name);

    //    if (theTag == null)
    //    {
    //        throw new ArgumentNullException("Veri boş");
    //    }

    //    return theTag;
    //}
}
