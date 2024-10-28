using System.Linq.Expressions;
using TripLogServer.Domain.Abstractions;
using TripLogServer.Domain.Entities;

namespace TripLogServer.Domain.Repositories;
public interface ITripRepository : IRepository<Trip>
{
    IQueryable<Trip> GetAllTripWithContents();
    Trip FirstOrDefault(Expression<Func<Trip, bool>> expression);
}
