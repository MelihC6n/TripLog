using System.Linq.Expressions;


namespace TripLogServer.Domain.Abstractions;
public interface IRepository<T> where T : class
{
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    bool Any(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAll();
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(T entity, CancellationToken cancellationToken);
    Task UpdateAsync(T entity, CancellationToken cancellationToken);
    Task DeleteAsync(T entity, CancellationToken cancellationToken);
}