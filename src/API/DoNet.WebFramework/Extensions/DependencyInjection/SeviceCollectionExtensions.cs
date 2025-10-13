using DoNet.Application.Extensions.DependencyInjection;
using DoNet.Infrastructure.Persistence.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoNet.WebFramework.Extensions.DependencyInjection;

public static class SeviceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();

        services.AddPersistence(configuration);

        // Register Swagger
        services.AddSwaggerWithJwtAuth(
            title: "DoNet API",
            version: "null",
            description: "DoNet API documentation"
        );

        return services;
    }
}
