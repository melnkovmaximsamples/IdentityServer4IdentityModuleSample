using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModule.Entities;
using IdentityModule.Shared;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityModule.Web.AppStart
{
    public class IdentityServerConfig
    {
        public static IEnumerable<Client> GetClients()
        {
            var minecraftWeb = AppData.Clients.MinecraftWeb.Value;

            return new List<Client>()
            {
                new Client()
                {
                    ClientId = minecraftWeb.ClientId,
                    ClientSecrets = { new Secret(minecraftWeb.ClientSecret.ToSha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        minecraftWeb.MainScope
                    },
                    IdentityTokenLifetime = 21600, // in seconds 6 hours
                    AuthorizationCodeLifetime = 21600,
                    AccessTokenLifetime = 15,                    
                    //RefreshTokenUsage = TokenUsage.ReUse,
                    //RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = 120,//1296000, //in seconds = 15 days
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    RequireConsent = false,
                    RedirectUris = { minecraftWeb.RedirectUri },
                    PostLogoutRedirectUris = { minecraftWeb.LogoutRedirectUri }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource(AppData.Clients.MinecraftWeb.Value.MainScope)
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiScope> GetScopes()
        {
            yield return new ApiScope(AppData.Clients.MinecraftWeb.Value.MainScope);
        }
    }
}
