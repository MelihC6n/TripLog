using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TripLogServer.Infrastructure.Abstractions;
using TripLogServer.Infrastructure.Context;

namespace TripLogServer.Infrastructure.Repositories;
internal sealed class TripContentRepository : Repository<TripContent, ApplicationDbContext>, ITripContentRepository
{
    public TripContentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
