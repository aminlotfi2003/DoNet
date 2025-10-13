using FluentValidation;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DoNet.Application.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register Commands and Queries
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Register Mappings
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Register Validations
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}
