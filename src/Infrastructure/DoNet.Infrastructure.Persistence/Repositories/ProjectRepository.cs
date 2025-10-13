using DoNet.Application.Abstractions.Repositories;
using DoNet.Domain.Entities;
using DoNet.Infrastructure.Persistence.Contexts;

namespace DoNet.Infrastructure.Persistence.Repositories;

public class ProjectRepository : EfRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext db) : base(db) { }
}
