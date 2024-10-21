using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TripLogServer.Infrastructure.Abstractions;
using TripLogServer.Infrastructure.Context;

namespace TripLogServer.Infrastructure.Repositories;
internal sealed class TripRepository : Repository<Trip, ApplicationDbContext>, ITripRepository
{
    public TripRepository(ApplicationDbContext context) : base(context)
    {
    }
}
