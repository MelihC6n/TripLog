using Microsoft.EntityFrameworkCore;
using TripLogServer.Domain.Abstractions;

namespace TripLogServer.Infrastructure.Abstractions;
public abstract class Repository<T, Db> : IRepository<T> where T : class where Db : DbContext
{
    private readonly Db context;
    public Repository(Db context)
    {
        this.context = context;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().FindAsync(id, cancellationToken);
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
