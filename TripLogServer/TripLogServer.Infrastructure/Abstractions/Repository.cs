using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TripLogServer.Domain.Abstractions;

namespace TripLogServer.Infrastructure.Abstractions;
public abstract class Repository<T, Db> : IRepository<T> where T : class where Db : DbContext
{
    private readonly Db context;
    private DbSet<T> entity;
    public Repository(Db context)
    {
        this.context = context;
        entity = context.Set<T>();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return entity.Where(expression).AsQueryable();
    }

    public IQueryable<T> WhereNoTracking(Expression<Func<T, bool>> expression)
    {
        return entity.Where(expression).AsNoTracking().AsQueryable();
    }

    public bool Any(Expression<Func<T, bool>> expression)
    {
        return entity.Any(expression);
    }

    public IQueryable<T> GetAll()
    {
        return entity.AsQueryable();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await entity.FindAsync(id, cancellationToken);
    }

    public async Task CreateAsync(T entity, CancellationToken cancellationToken)
    {
        await context.AddAsync(entity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        context.Update(entity);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        context.Remove(entity);
        await context.SaveChangesAsync(cancellationToken);
    }
}
