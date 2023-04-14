using GlobalBlueHomework.Application.Options;

namespace GlobalBlueHomework.Api.DependencyInjections;

public static class ApiDependencyInjection
{
    /// <summary>
    /// Add API layer dependency injections.
    /// </summary>
    /// <returns>Returns the <paramref name="services"/> object incremented with services.</returns>
    public static IServiceCollection AddApiDependencyInjections(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureAutoMapper();
        services.AddApiOptions(configuration);

        return services;
    }

    /// <summary>
    /// Add Api layer services.
    /// </summary>
    /// <returns>Returns the <paramref name="services"/> object incremented with services.</returns>
    private static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }

    /// <summary>
    /// Add Api options.
    /// </summary>
    /// <returns>Returns the <paramref name="services"/> object incremented with services.</returns>
    private static IServiceCollection AddApiOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppOptions>(configuration);
        return services;
    }
}