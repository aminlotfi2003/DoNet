using DoNet.Application.Abstractions.Repositories;
using DoNet.Domain.Entities;
using DoNet.Infrastructure.Persistence.Contexts;

namespace DoNet.Infrastructure.Persistence.Repositories;

public class CommentRepository : EfRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext db) : base(db) { }
}
