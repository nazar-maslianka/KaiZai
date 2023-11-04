using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace KaiZai.Service.Identity.API.Quickstart
{
    public static class IdentityBaseConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "kaizai-user",
                    ClientSecrets = new [] { new Secret("kaizai_secret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "KaiZai.Service.Identity.API" }
                }
            };
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> 
            { 
                new ApiScope("KaiZai.Service.Identity.API", "KaiZaiIdentity API") 
            };
        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource> 
            { 
                new ApiResource("KaiZai.Service.Identity.API", "KaiZaiIdentity API") 
                { 
                    Scopes = { "KaiZai.Service.Identity.API" } 
                } 
            };
    }
}