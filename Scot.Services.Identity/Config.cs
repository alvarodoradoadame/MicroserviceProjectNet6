using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Scot.Services.Identity
{
    public static class Config
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("scot", "Scot Server"),
                new ApiScope(name:"read", displayName: "Read your data."),
                new ApiScope(name:"write", displayName: "Write your data."),
                new ApiScope(name:"delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "scope1" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = { "https://localhost:44300/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "scope2" }
                },
                new Client {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client {
                    ClientId = "scot",
                    ClientSecrets = { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "scot"
                    },
                    RedirectUris = { "https://localhost:7000/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:7000/signout-callback-oidc" }
                }
            };
    }
}



