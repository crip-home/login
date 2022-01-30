namespace Crip.Login;

using Configuration;

public class Startup
{
    public IWebHostEnvironment Environment { get; }
    public IConfiguration Configuration { get; }

    public Startup(IWebHostEnvironment environment, IConfiguration configuration)
    {
        Environment = environment;
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSpa()
            .AddControllers()
            .AddCors(options =>
            {
                options.AddVueCors();
            })
            .AddIdentityServer(Configuration)
            .AddAuthorization();
    }

    public void Configure(IApplicationBuilder app)
    {
        if (Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(SpaConfiguration.VueCorsPolicy);

        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
        });

        app.UseSpaStaticFiles();
        app.UseSpa(SpaConfiguration.UseIn(Environment));
    }
}