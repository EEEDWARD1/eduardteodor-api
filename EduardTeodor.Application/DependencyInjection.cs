using EduardTeodor.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;

namespace EduardTeodor.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<GetAllPublishedPosts>();
        services.AddScoped<GetPublishedPost>();

        return services;
    }
}
