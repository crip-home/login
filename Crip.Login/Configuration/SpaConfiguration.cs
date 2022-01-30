using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.StaticFiles;

namespace Crip.Login.Configuration;

public static class SpaConfiguration
{
    public const string VueCorsPolicy = nameof(VueCorsPolicy);

    public static IServiceCollection AddSpa(this IServiceCollection services)
    {
        services.AddSpaStaticFiles(ConfigureSpa);
        return services;
    }

    public static void AddVueCors(this CorsOptions options)
    {
        options.AddPolicy(VueCorsPolicy, builder =>
        {
            builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins("https://localhost:7091");
        });
    }

    public static Action<ISpaBuilder> UseIn(IWebHostEnvironment env) => builder =>
    {
        if (env.IsDevelopment())
        {
            builder.UseProxyToSpaDevelopmentServer("http://localhost:8080");
        }
    };

    private static void ConfigureSpa(SpaStaticFilesOptions options)
    {
        options.RootPath = "wwwroot";
    }
}