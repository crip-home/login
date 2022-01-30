using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Crip.Login.Configuration;

public static class ControllerConfiguration
{
    public static IServiceCollection AddControllers(this IServiceCollection services)
    {
        services
            .AddHttpContextAccessor()
            .AddActionContextAccessor()
            .AddControllers(mvc => {})
            .AddNewtonsoftJson(SetupNewtonsoftJson);

        return services;
    }

    private static void SetupNewtonsoftJson(MvcNewtonsoftJsonOptions options)
    {
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    }

    private static IServiceCollection AddActionContextAccessor(this IServiceCollection services)
    {
        services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
        return services;
    }
}