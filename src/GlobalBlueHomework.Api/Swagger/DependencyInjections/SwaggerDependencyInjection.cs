using GlobalBlueHomework.Api.Swagger.DependencyInjections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace GlobalBlueHomework.Api.Swagger.DependencyInjections;

public static class SwaggerDependencyInjection
{
    private const string ApiVersionHeaderName = "X-Api-Version";

    /// <summary>
    /// Add Swagger to the project.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="configuration"></param>
    /// <returns>Returns the <paramref name="app"/> object incremented with Swagger.</returns>
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger(config =>
        {
            config.RouteTemplate = "api/swagger/{documentName}/swagger.json";
            //config.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(config =>
        {
            config.RoutePrefix = "api/swagger";

            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint(
                    $"/api/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }

        });

        return app;
    }

    /// <summary>
    /// Configure Swagger.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns>Returns the <paramref name="services"/> object incremented with Web API options.</returns>
    public static IServiceCollection ConfigureSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(opt =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            opt.IncludeXmlComments(xmlPath);
            opt.EnableAnnotations();
            opt.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");
        });

        services.AddVersioning();

        return services;
    }

    /// <summary>
    /// Configure automatic API versioning.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    internal static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);

            // Read the api version from the header request
            options.ApiVersionReader = new HeaderApiVersionReader(ApiVersionHeaderName);

            // If the client hasn't specified the API version in the request, use the default API version number
            options.AssumeDefaultVersionWhenUnspecified = true;

            // Advertise the API versions supported for the particular endpoint. Return headers "api-supported-versions" and "api-deprecated-versions".
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
            // note: the specified format code will format the version as "'v'major[.minor][-status]"
            options.GroupNameFormat = "'v'VVV";

            // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
            // can also be used to control the format of the API version in route templates
            options.SubstituteApiVersionInUrl = true;
        });

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerDocumentOptions>();

        return services;
    }
}