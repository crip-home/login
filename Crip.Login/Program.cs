using Serilog;

namespace Crip.Login;

public static class Program
{
    public static int Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(ReadConfiguration())
            .CreateLogger();

        try
        {
            CreateHostBuilder(args).Build().Run();
            return 0;
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "Application start-up failed");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureAppConfiguration(ConfigureApp)
            .ConfigureWebHostDefaults(ConfigureWeb);

    private static IConfigurationRoot ReadConfiguration() =>
        Configure(NewConfiguration()).Build();

    private static IConfigurationBuilder NewConfiguration() =>
        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{GetEnvironment()}.json", true);

    private static string GetEnvironment() =>
        Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

    private static void ConfigureApp(HostBuilderContext context, IConfigurationBuilder config) =>
        Configure(config);

    private static IConfigurationBuilder Configure(IConfigurationBuilder config) => config
        .AddJsonFile("settings/appsettings.json", optional: true, reloadOnChange: true);

    private static void ConfigureWeb(IWebHostBuilder builder) => builder
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseStartup<Startup>();
}