using DoNet.Application.Abstractions.Repositories;
using DoNet.Application.Abstractions.UoW;
using DoNet.Infrastructure.Persistence.Contexts;
using DoNet.Infrastructure.Persistence.Repositories;
using DoNet.Infrastructure.Persistence.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoNet.Infrastructure.Persistence.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Register DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        // Register Repositories
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        // Register Unit of Work
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        return services;
    }
}
