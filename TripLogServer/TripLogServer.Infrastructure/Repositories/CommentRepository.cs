using TripLogServer.Domain.Entities;
using TripLogServer.Domain.Repositories;
using TripLogServer.Infrastructure.Abstractions;
using TripLogServer.Infrastructure.Context;

namespace TripLogServer.Infrastructure.Repositories;
internal sealed class CommentRepository : Repository<Comment, ApplicationDbContext>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext context) : base(context)
    {
    }
}
