using DoNet.Application.Extensions.DependencyInjection;
using DoNet.Infrastructure.Persistence.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoNet.WebFramework.Extensions.DependencyInjection;

public static class SeviceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Dependencies Layers
        services.AddApplication()
                .AddPersistence(configuration);

        // Register API Versioning
        services.AddApiVersioningDependencies();

        // Register Swagger
        services.AddSwaggerWithJwtAuth(
            title: "DoNet API",
            version: "v1",
            description: "DoNet API documentation"
        );

        // Register Services
        services.AddAuthentication();
        services.AddHttpContextAccessor();

        return services;
    }
}
