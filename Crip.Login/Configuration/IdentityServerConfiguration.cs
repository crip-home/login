using Duende.IdentityServer;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Models;

namespace Crip.Login.Configuration;

public static class IdentityServerConfiguration
{
    public static IServiceCollection AddIdentityServer(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddIdentityServer(SetupIdentityServer)
            .AddInMemoryClients(Clients(config))
            .AddInMemoryIdentityResources(IdentityResources)
            .AddInMemoryApiScopes(ApiScopes)
            .AddInMemoryApiResources(ApiResources);

        return services;
    }

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new()
        {
            Name = "https://crip.com/security/identity",
            Enabled = true,
            DisplayName = "Identity resource",
            Scopes = { "api:identity" }
        }
    };

    private static IConfigurationSection Clients(IConfiguration config) => config.GetSection("IdentityServer:Clients");

    private static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new()
        {
            Name = "api:identity",
            Enabled = true,
            Description = "Identity API",
            ShowInDiscoveryDocument = true,
        }
    };

    private static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile(),
        new IdentityResources.Email(),
    };

    private static void SetupIdentityServer(IdentityServerOptions options)
    {
    }
}