using FluentValidation;
using GlobalBlueHomework.Application.Services.Purchases.Calculator;
using GlobalBlueHomework.Application.Services.Vat;
using Microsoft.Extensions.DependencyInjection;

namespace GlobalBlueHomework.Application.DependencyInjections;

public static class ApplicationDependencyInjection
{
    /// <summary>
    /// Add application layer dependency injections.
    /// </summary>
    /// <returns>Returns the <paramref name="services"/> object incremented with application layer services.</returns>
    public static IServiceCollection AddApplicationDependencyInjections(this IServiceCollection services)
    {
        services.AddApplicationServices();
        services.AddApplicationValidators();

        return services;
    }

    /// <summary>
    /// Add application layer services.
    /// </summary>
    /// <returns>Returns the <paramref name="services"/> object incremented with application layer services.</returns>
    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<ICalculatePurchaseAmountsService, CalculatePurchaseAmountsService>();
        services.AddTransient<IGetCountryValidVatRatesService, GetCountryValidVatRatesService>();

        return services;
    }

    /// <summary>
    /// Add application layer validators.
    /// </summary>
    /// <returns>Returns the <paramref name="services"/> object incremented with application layer services.</returns>
    private static IServiceCollection AddApplicationValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(ApplicationDependencyInjection));

        return services;
    }
}