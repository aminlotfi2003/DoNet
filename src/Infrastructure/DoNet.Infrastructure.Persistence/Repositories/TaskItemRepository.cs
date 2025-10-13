using DoNet.Application.Abstractions.Repositories;
using DoNet.Domain.Entities;
using DoNet.Infrastructure.Persistence.Contexts;

namespace DoNet.Infrastructure.Persistence.Repositories;

public class TaskItemRepository : EfRepository<TaskItem>, ITaskItemRepository
{
    public TaskItemRepository(ApplicationDbContext db) : base(db) { }
}
