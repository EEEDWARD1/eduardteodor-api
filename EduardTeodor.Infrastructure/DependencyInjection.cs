using EduardTeodor.Application.Interfaces;
using EduardTeodor.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace EduardTeodor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddNpgsqlDataSource(connectionString);
        services.AddScoped<IPostRepository, PostRepository>();

        return services;
    }
}